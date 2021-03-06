using System;
using Newtonsoft.Json;
using StardewModdingAPI.Common;

namespace StardewModdingAPI
{
    /// <summary>A semantic version with an optional release tag.</summary>
    public class SemanticVersion : ISemanticVersion
    {
        /*********
        ** Properties
        *********/
        /// <summary>The underlying semantic version implementation.</summary>
        private readonly SemanticVersionImpl Version;


        /*********
        ** Accessors
        *********/
        /// <summary>The major version incremented for major API changes.</summary>
        public int MajorVersion => this.Version.Major;

        /// <summary>The minor version incremented for backwards-compatible changes.</summary>
        public int MinorVersion => this.Version.Minor;

        /// <summary>The patch version for backwards-compatible bug fixes.</summary>
        public int PatchVersion => this.Version.Patch;

        /// <summary>An optional build tag.</summary>
        public string Build => this.Version.Tag;


        /*********
        ** Public methods
        *********/
        /// <summary>Construct an instance.</summary>
        /// <param name="majorVersion">The major version incremented for major API changes.</param>
        /// <param name="minorVersion">The minor version incremented for backwards-compatible changes.</param>
        /// <param name="patchVersion">The patch version for backwards-compatible bug fixes.</param>
        /// <param name="build">An optional build tag.</param>
        [JsonConstructor]
        public SemanticVersion(int majorVersion, int minorVersion, int patchVersion, string build = null)
            : this(new SemanticVersionImpl(majorVersion, minorVersion, patchVersion, build)) { }

        /// <summary>Construct an instance.</summary>
        /// <param name="version">The semantic version string.</param>
        /// <exception cref="ArgumentNullException">The <paramref name="version"/> is null.</exception>
        /// <exception cref="FormatException">The <paramref name="version"/> is not a valid semantic version.</exception>
        public SemanticVersion(string version)
            : this(new SemanticVersionImpl(version)) { }

        /// <summary>Get an integer indicating whether this version precedes (less than 0), supercedes (more than 0), or is equivalent to (0) the specified version.</summary>
        /// <param name="other">The version to compare with this instance.</param>
        /// <exception cref="ArgumentNullException">The <paramref name="other"/> value is null.</exception>
        /// <remarks>The implementation is defined by Semantic Version 2.0 (http://semver.org/).</remarks>
        public int CompareTo(ISemanticVersion other)
        {
            return this.Version.CompareTo(other.MajorVersion, other.MinorVersion, other.PatchVersion, other.Build);
        }

        /// <summary>Get whether this version is older than the specified version.</summary>
        /// <param name="other">The version to compare with this instance.</param>
        public bool IsOlderThan(ISemanticVersion other)
        {
            return this.CompareTo(other) < 0;
        }

        /// <summary>Get whether this version is older than the specified version.</summary>
        /// <param name="other">The version to compare with this instance.</param>
        /// <exception cref="FormatException">The specified version is not a valid semantic version.</exception>
        public bool IsOlderThan(string other)
        {
            return this.IsOlderThan(new SemanticVersion(other));
        }

        /// <summary>Get whether this version is newer than the specified version.</summary>
        /// <param name="other">The version to compare with this instance.</param>
        public bool IsNewerThan(ISemanticVersion other)
        {
            return this.CompareTo(other) > 0;
        }

        /// <summary>Get whether this version is newer than the specified version.</summary>
        /// <param name="other">The version to compare with this instance.</param>
        /// <exception cref="FormatException">The specified version is not a valid semantic version.</exception>
        public bool IsNewerThan(string other)
        {
            return this.IsNewerThan(new SemanticVersion(other));
        }

        /// <summary>Get whether this version is between two specified versions (inclusively).</summary>
        /// <param name="min">The minimum version.</param>
        /// <param name="max">The maximum version.</param>
        public bool IsBetween(ISemanticVersion min, ISemanticVersion max)
        {
            return this.CompareTo(min) >= 0 && this.CompareTo(max) <= 0;
        }

        /// <summary>Get whether this version is between two specified versions (inclusively).</summary>
        /// <param name="min">The minimum version.</param>
        /// <param name="max">The maximum version.</param>
        /// <exception cref="FormatException">One of the specified versions is not a valid semantic version.</exception>
        public bool IsBetween(string min, string max)
        {
            return this.IsBetween(new SemanticVersion(min), new SemanticVersion(max));
        }

        /// <summary>Indicates whether the current object is equal to another object of the same type.</summary>
        /// <returns>true if the current object is equal to the <paramref name="other" /> parameter; otherwise, false.</returns>
        /// <param name="other">An object to compare with this object.</param>
        public bool Equals(ISemanticVersion other)
        {
            return other != null && this.CompareTo(other) == 0;
        }

        /// <summary>Get a string representation of the version.</summary>
        public override string ToString()
        {
            return this.Version.ToString();
        }

        /// <summary>Parse a version string without throwing an exception if it fails.</summary>
        /// <param name="version">The version string.</param>
        /// <param name="parsed">The parsed representation.</param>
        /// <returns>Returns whether parsing the version succeeded.</returns>
        internal static bool TryParse(string version, out ISemanticVersion parsed)
        {
            if (SemanticVersionImpl.TryParse(version, out SemanticVersionImpl versionImpl))
            {
                parsed = new SemanticVersion(versionImpl);
                return true;
            }

            parsed = null;
            return false;
        }


        /*********
        ** Private methods
        *********/
        /// <summary>Construct an instance.</summary>
        /// <param name="version">The underlying semantic version implementation.</param>
        private SemanticVersion(SemanticVersionImpl version)
        {
            this.Version = version;
        }

    }
}
