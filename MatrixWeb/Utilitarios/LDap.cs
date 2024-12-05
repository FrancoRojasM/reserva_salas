
//using System.Collections.Generic;
//using System.Linq;
//using System.Collections.ObjectModel;
//using System.Collections;
//using System;
//using System.Globalization;
//namespace MatrixWebApp.Utilitarios
//{
  


//    /// <summary>
//    /// Represents an LDAP entry.
//    /// </summary>
//    public class LdapEntry : IReadOnlyDictionary<string, LdapAttribute>
//    {
//        /// <summary>
//        /// Private backing field for Attributes.
//        /// </summary>
//        private readonly ReadOnlyDictionary<string, LdapAttribute> attributes;

//        /// <summary>
//        /// Private backing field for DistinguishedName.
//        /// </summary>
//        private readonly string distinguishedName;

//        /// <summary>
//        /// Initializes a new instance of the <see cref="LdapEntry" /> structure.
//        /// </summary>
//        /// <param name="distinguishedName">The distinguished name for the entry.</param>
//        /// <param name="attributes">The attributes for the entry.</param>
//        public LdapEntry(string distinguishedName, IDictionary<string, LdapAttribute> attributes)
//        {
//            this.distinguishedName = distinguishedName;
//            this.attributes = new ReadOnlyDictionary<string, LdapAttribute>(attributes);
//        }

//        /// <summary>
//        /// Gets the attributes for the entry.
//        /// </summary>
//        /// <value>The <see cref="LdapAttribute"/>s for the <see cref="LdapEntry"/>.</value>
//        public ReadOnlyDictionary<string, LdapAttribute> Attributes => this.attributes;

//        /// <summary>
//        /// Gets the number of elements in the collection.
//        /// </summary>
//        public int Count => this.attributes.Count;

//        /// <summary>
//        /// Gets the distinguished name for the entry.
//        /// </summary>
//        /// <value>The distinguished name for the entry.</value>
//        public string DistinguishedName => this.distinguishedName;

//        /// <summary>
//        /// Gets an enumerable collection that contains the attribute types in the <see cref="LdapEntry"/>.
//        /// </summary>
//        public IEnumerable<string> Keys => this.attributes.Keys;

//        /// <summary>
//        /// Gets an enumerable collection that contains the <see cref="LdapAttribute"/>s in the <see cref="LdapEntry"/>.
//        /// </summary>
//        public IEnumerable<LdapAttribute> Values => this.attributes.Values;

//        /// <summary>
//        /// Gets the <see cref="LdapAttribute"/> that has the specified attribute type in the <see cref="LdapEntry"/>.
//        /// </summary>
//        /// <param name="key">The <see cref="LdapAttribute"/> to locate.</param>
//        /// <returns>The <see cref="LdapAttribute"/> that has the specified attribute type in the <see cref="LdapEntry"/>.</returns>
//        public LdapAttribute this[string key] => this.attributes[key];

//        /// <summary>
//        /// Determines whether the <see cref="LdapEntry"/> contains an attribute type that has the specified name.
//        /// </summary>
//        /// <param name="key">The attribute type to locate.</param>
//        /// <returns><b>true</b> if the <see cref="LdapEntry"/> contains an attribute type that has the specified name; otherwise, <b>false</b>.</returns>
//        public bool ContainsKey(string key) => this.attributes.ContainsKey(key);

//        /// <summary>
//        /// Returns an enumerator that iterates through the collection of <see cref="LdapAttribute"/>s.
//        /// </summary>
//        /// <returns>An enumerator that can be used to iterate through the collection of <see cref="LdapAttribute"/>s.</returns>
//        public IEnumerator<KeyValuePair<string, LdapAttribute>> GetEnumerator() => this.attributes.GetEnumerator();

//        /// <summary>
//        /// Returns an enumerator that iterates through the collection of <see cref="LdapAttribute"/>s.
//        /// </summary>
//        /// <returns>An enumerator that can be used to iterate through the collection of <see cref="LdapAttribute"/>s.</returns>
//        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

//        /// <summary>
//        /// Gets the value that is associated with the specified attribute type.
//        /// </summary>
//        /// <param name="key">The attribute type to locate.</param>
//        /// <param name="value">
//        /// When this method returns, the value associated with the specified attribute type, if the attribute namer is found;
//        /// otherwise, the default value for the type of the value parameter. This parameter is passed uninitialized.
//        /// </param>
//        /// <returns><b>true</b> if the <see cref="LdapEntry"/> contains an attribute type that has the specified name; otherwise, <b>false</b>.</returns>
//        public bool TryGetValue(string key, out LdapAttribute value) => this.attributes.TryGetValue(key, out value);
//    }


//    /// <summary>
//    /// Represents an LDAP attribute type with both decoded and encoded values.
//    /// </summary>
//    public struct LdapAttribute
//    {
//        /// <summary>
//        /// Private backing field for Name.
//        /// </summary>
//        private readonly string name;

//        /// <summary>
//        /// Private backing field for decoded data.
//        /// </summary>
//        private readonly ReadOnlyCollection<string> values;

//        /// <summary>
//        /// Private backing field for encoded data.
//        /// </summary>
//        private readonly ReadOnlyCollection<byte[]> valuesBytes;

//        /// <summary>
//        /// Initializes a new instance of the <see cref="LdapAttribute" /> structure.
//        /// </summary>
//        /// <param name="name">The attribute type.</param>
//        /// <param name="values">The decoded attribute values.</param>
//        /// <param name="valuesBytes">The encoded attribute values.</param>
//        public LdapAttribute(string name, IEnumerable<string> values, IEnumerable<byte[]> valuesBytes)
//        {
//            this.name = name;
//            this.values = new ReadOnlyCollection<string>(values.ToList());
//            this.valuesBytes = new ReadOnlyCollection<byte[]>(valuesBytes.ToList());
//        }

//        /// <summary>
//        /// Gets the attribute type.
//        /// </summary>
//        /// <value>The attribute type.</value>
//        public string Name => this.name;

//        /// <summary>
//        /// Gets the first decoded value.
//        /// </summary>
//        /// <value>The first decoded value.</value>
//        public string Value => this.values[0];

//        /// <summary>
//        /// Gets the first encoded value.
//        /// </summary>
//        /// <value>The first encoded value.</value>
//        public byte[] ValueBytes => this.valuesBytes[0];

//        /// <summary>
//        /// Gets the collection of decoded values.
//        /// </summary>
//        /// <value>The collection of decoded values.</value>
//        public ReadOnlyCollection<string> Values => this.values;

//        /// <summary>
//        /// Gets the collection of encoded values.
//        /// </summary>
//        /// <value>The collection of encoded values.</value>
//        public ReadOnlyCollection<byte[]> ValuesBytes => this.valuesBytes;
//    }

//    public class LdapSearch
//    {
//        /// <summary>
//        /// The associated LDAP connection.
//        /// </summary>
//        private readonly LdapConnection ldapConnection;

//        /// <summary>
//        /// Private backing field for AttributeList.
//        /// </summary>
//        private IEnumerable<string> attributeList;

//        /// <summary>
//        /// Private backing field for BaseDn.
//        /// </summary>
//        private string baseDn;

//        /// <summary>
//        /// Private backing field for LdapFilter.
//        /// </summary>
//        private string ldapFilter;

//        /// <summary>
//        /// Private backing field for PageSize.
//        /// </summary>
//        private int pageSize;

//        /// <summary>
//        /// Private backing field for RangedRetrieval.
//        /// </summary>
//        private bool rangedRetrieval;

//        /// <summary>
//        /// Private backing field for Referrals.
//        /// </summary>
//        private bool referrals;

//        /// <summary>
//        /// Private backing field for SearchScope.
//        /// </summary>
//        private SearchScope searchScope;

//        /// <summary>
//        /// Private backing field for SizeLimit.
//        /// </summary>
//        private int sizeLimit;

//        /// <summary>
//        /// Initializes a new instance of the <see cref="LdapSearch"/> class.
//        /// </summary>
//        /// <param name="ldapConnection"></param>
//        public LdapSearch(LdapConnection ldapConnection) => this.ldapConnection = ldapConnection;

//        /// <summary>
//        /// Gets or sets a value representing the attribute list to retrieve during a search operation.
//        /// </summary>
//        /// <value>The attribute list to retrieve during a search operation.</value>
//        public IEnumerable<string> AttributeList
//        {
//            get => this.attributeList;

//            set => this.attributeList = value;
//            }

//        /// <summary>
//        /// Gets or sets a value representing the object at which to start the search.
//        /// </summary>
//        /// <value>The object at which to start the search.</value>
//        public string BaseDn
//        {
//            get => this.baseDn;

//            set => this.baseDn = value;
//            }

//        /// <summary>
//        /// Gets or sets a value representing the LDAP search filter.
//        /// </summary>
//        /// <value>The LDAP search filter.</value>
//        public string LdapFilter
//        {
//            get => this.ldapFilter;

//            set => this.ldapFilter = value;
//            }

//        /// <summary>
//        /// Gets or sets a value representing the size of the paged results control.
//        /// </summary>
//        /// <value>The size of the paged results control, 0 disabled the control.</value>
//        public int PageSize
//        {
//            get => this.pageSize;

//            set => this.pageSize = value;
//            }

//        /// <summary>
//        /// Gets or sets a value indicating whether ranged retrieval is enabled.
//        /// </summary>
//        /// <value><true>true</true> if ranged retrieval is enabled, otherwise <b>false</b>.</value>
//        public bool RangedRetrieval
//        {
//            get => this.rangedRetrieval;

//            set => this.rangedRetrieval = value;
//            }

//        /// <summary>
//        /// Gets or sets a value indicating whether referrals are enabled.
//        /// </summary>
//        /// <value><true>true</true> if referrals are enabled, otherwise <b>false</b>.</value>
//        public bool Referrals
//        {
//            get => this.referrals;

//            set => this.referrals = value;
//            }

//        /// <summary>
//        /// Gets or sets a value representing the scope of a search.
//        /// </summary>
//        /// <value>The scope of a search.</value>
//        public SearchScope SearchScope
//        {
//            get => this.searchScope;

//            set => this.searchScope = value;
//            }

//        /// <summary>
//        /// Gets or sets a value indicating the maximum number of entries the directory service agent will return when answering a search request.
//        /// </summary>
//        /// <value>The maximum number of entries the directory service agent will return when answering a search request.</value>
//        public int SizeLimit
//        {
//            get => this.sizeLimit;

//            set => this.sizeLimit = value;
//            }

//        /// <summary>
//        /// Initiates the LDAP search.
//        /// </summary>
//        /// <returns>The LDAP search result set.</returns>
//        public IEnumerable<LdapEntry> Search()
//        {
//            // Validate search request requirements.
//            if (string.IsNullOrWhiteSpace(this.baseDn))
//            {
//                throw new ArgumentOutOfRangeException(nameof(this.BaseDn), "The BaseDn must be specified.");
//            }

//            if (string.IsNullOrWhiteSpace(this.ldapFilter))
//            {
//                throw new ArgumentOutOfRangeException(nameof(this.LdapFilter), "The LDAP filter must be specified.");
//            }

//            // Setup search request.
//            SearchRequest searchRequest = new SearchRequest(
//                this.baseDn,
//                this.ldapFilter,
//                this.searchScope,
//                this.attributeList as string[]);

//            // Configure size limit.
//            if (this.sizeLimit > 0)
//            {
//                searchRequest.SizeLimit = this.sizeLimit;
//            }

//            // Configure referrals.
//            if (!this.referrals)
//            {
//                SearchOptionsControl searchOptions = new SearchOptionsControl(SearchOption.DomainScope);
//                searchRequest.Controls.Add(searchOptions);
//            }

//            // Assign search delegate.
//            Func<SearchRequest, IEnumerable<SearchResultEntry>> searchFunc;
//            if (this.pageSize > 0)
//            {
//                // Paged search enabled.
//                searchFunc = this.SearchPaged;
//            }
//            else
//            {
//                // Simple search enabled.
//                searchFunc = this.SearchSimple;
//            }

//            // Assign parser delegate.
//            Func<SearchResultEntry, Dictionary<string, LdapAttribute>> parser;
//            if (this.RangedRetrieval)
//            {
//                parser = searchResultEntry => this.ParseSearchResultEntryRanged(searchFunc, searchResultEntry);
//            }
//            else
//            {
//                parser = this.ParseSearchResultEntry;
//            }

//            // Invoke search.
//            foreach (SearchResultEntry searchResultEntry in searchFunc(searchRequest))
//            {
//                Dictionary<string, LdapAttribute> attributes = parser(searchResultEntry);

//                yield return new LdapEntry(searchResultEntry.DistinguishedName, attributes);
//            }
//        }

//        /// <summary>
//        /// Detects if an attribute type returned by the directory has had ranged retrieval invoked.
//        /// </summary>
//        /// <param name="attributeType">The attribute type returned by the directory to parse.</param>
//        /// <param name="attributeTypeActual">The actual parsed name.</param>
//        /// <param name="start">The start range, or 0 if unavailable.</param>
//        /// <param name="end">The end range, or 0 if unavailable or if the range is complete.</param>
//        /// <returns><true>true</true> if the directory has had invoked ranged retrieval, otherwise <b>false</b>.</returns>
//        private bool ParseRange(string attributeType, out string attributeTypeActual, out ulong start, out ulong end)
//        {
//            // Validate input.
//            if (string.IsNullOrWhiteSpace(attributeType))
//            {
//                throw new LdapException("Directory returned to an invalid attribute type.");
//            }

//            // Detect if directory has invoked range retrieval.
//            int rangeIndex = attributeType.IndexOf(";range=", StringComparison.OrdinalIgnoreCase);
//            if (rangeIndex == -1)
//            {
//                attributeTypeActual = attributeType;
//                start = 0;
//                end = 0;

//                return false;
//            }

//            // Update actual attribute type.
//            attributeTypeActual = attributeType.Substring(0, rangeIndex);

//            // Extract range start and end.
//            string[] range = attributeType
//                .Substring(rangeIndex + 7)
//                .Split('-');

//            if (range.Length != 2)
//            {
//                throw new LdapException("Failed to parse ranged attribute.");
//            }

//            string rangeStartString = range[0];
//            string rangeEndString = range[1];

//            // Parse start value.
//            if (!ulong.TryParse(rangeStartString, NumberStyles.None, CultureInfo.InvariantCulture, out ulong rangeStart))
//            {
//                throw new LdapException("Failed to parse start of ranged attribute.");
//            }

//            // Detect end of range or parse end value if needed.
//            if (rangeEndString == "*")
//            {
//                start = 0;
//                end = 0;

//                return false;
//            }

//            if (!ulong.TryParse(rangeEndString, NumberStyles.None, CultureInfo.InvariantCulture, out ulong rangeEnd))
//            {
//                throw new LdapException("Failed to parse end of ranged attribute.");
//            }

//            start = rangeStart;
//            end = rangeEnd;

//            return true;
//        }

//        /// <summary>
//        /// Converts the <see cref="DirectoryAttribute"/>s from a search result into <see cref="LdapAttribute"/>s.
//        /// </summary>
//        /// <param name="searchResultEntry">The search result entry to convert.</param>
//        /// <returns>The entries LDAP attributes.</returns>
//        private Dictionary<string, LdapAttribute> ParseSearchResultEntry(SearchResultEntry searchResultEntry)
//        {
//            Dictionary<string, LdapAttribute> ldapAttributes = new Dictionary<string, LdapAttribute>();

//            if (searchResultEntry?.Attributes?.Values == null)
//            {
//                return ldapAttributes;
//            }

//            // Collect raw attribute values.
//            Dictionary<string, Tuple<List<string>, List<byte[]>>> rawAttributes = new Dictionary<string, Tuple<List<string>, List<byte[]>>>();
//            foreach (DirectoryAttribute directoryAttribute in searchResultEntry.Attributes.Values)
//            {
//                // Prepare collection references.
//                List<string> valuesString;
//                List<byte[]> valuesByte;
//                Tuple<List<string>, List<byte[]>> rawAttribute;
//                if (rawAttributes.TryGetValue(directoryAttribute.Name, out rawAttribute))
//                {
//                    valuesString = rawAttribute.Item1;
//                    valuesByte = rawAttribute.Item2;
//                }
//                else
//                {
//                    valuesString = new List<string>();
//                    valuesByte = new List<byte[]>();
//                    rawAttribute = new Tuple<List<string>, List<byte[]>>(valuesString, valuesByte);
//                    rawAttributes.Add(directoryAttribute.Name, rawAttribute);
//                }

//                // Collect initial appearance of values.
//                valuesString.AddRange((string[])directoryAttribute.GetValues(typeof(string)));
//                valuesByte.AddRange((byte[][])directoryAttribute.GetValues(typeof(byte[])));
//            }

//            // Create LDAP attributes.
//            foreach (KeyValuePair<string, Tuple<List<string>, List<byte[]>>> rawAttribute in rawAttributes)
//            {
//                LdapAttribute ldapAttribute = new LdapAttribute(rawAttribute.Key, rawAttribute.Value.Item1, rawAttribute.Value.Item2);
//                ldapAttributes.Add(rawAttribute.Key, ldapAttribute);
//            }

//            return ldapAttributes;
//        }

//        /// <summary>
//        /// Converts the <see cref="DirectoryAttribute"/>s from a search result into <see cref="LdapAttribute"/>s with automatic ranged retrieval.
//        /// </summary>
//        /// <param name="searchFunc">The configured search implementation for ranged retrieval.</param>
//        /// <param name="searchResultEntry">The search result entry to convert.</param>
//        /// <returns>The entries LDAP attributes.</returns>
//        private Dictionary<string, LdapAttribute> ParseSearchResultEntryRanged(Func<SearchRequest, IEnumerable<SearchResultEntry>> searchFunc, SearchResultEntry searchResultEntry)
//        {
//            Dictionary<string, LdapAttribute> ldapAttributes = new Dictionary<string, LdapAttribute>();

//            if (searchResultEntry?.Attributes?.Values == null)
//            {
//                return ldapAttributes;
//            }

//            // Collect raw attribute values.
//            Dictionary<string, Tuple<List<string>, List<byte[]>>> rawAttributes = new Dictionary<string, Tuple<List<string>, List<byte[]>>>();
//            foreach (DirectoryAttribute directoryAttribute in searchResultEntry.Attributes.Values)
//            {
//                string attributeTypeActual;
//                ulong start;
//                ulong end;
//                bool rangedRetrievalEnabled = this.ParseRange(directoryAttribute.Name, out attributeTypeActual, out start, out end);

//                // Prepare collection references.
//                List<string> valuesString;
//                List<byte[]> valuesByte;
//                Tuple<List<string>, List<byte[]>> rawAttribute;
//                if (rawAttributes.TryGetValue(attributeTypeActual, out rawAttribute))
//                {
//                    valuesString = rawAttribute.Item1;
//                    valuesByte = rawAttribute.Item2;
//                }
//                else
//                {
//                    valuesString = new List<string>();
//                    valuesByte = new List<byte[]>();
//                    rawAttribute = new Tuple<List<string>, List<byte[]>>(valuesString, valuesByte);
//                    rawAttributes.Add(attributeTypeActual, rawAttribute);
//                }

//                // Collect initial values.
//                valuesString.AddRange((string[])directoryAttribute.GetValues(typeof(string)));
//                valuesByte.AddRange((byte[][])directoryAttribute.GetValues(typeof(byte[])));

//                if (!rangedRetrievalEnabled)
//                {
//                    continue;
//                }

//                // Directory has invoked range retrieval, query the remaining values.
//                do
//                {
//                    // Setup search request for the next range interval.
//                    string rangedAttribute = $"{attributeTypeActual};range={end + 1}-*";
//                    SearchRequest searchRequest = new SearchRequest(
//                        searchResultEntry.DistinguishedName,
//                        "(objectClass=*)",
//                        SearchScope.Base,
//                        rangedAttribute);

//                    // Ensure one search result entry was returned without enumerating the entire collection.
//                    SearchResultEntry rangedSearchResultEntry = null;
//                    foreach (SearchResultEntry entry in searchFunc(searchRequest))
//                    {
//                        if (rangedSearchResultEntry != null)
//                        {
//                            throw new LdapException("Directory returned to many search results during ranged retrieval.");
//                        }

//                        rangedSearchResultEntry = entry;
//                    }

//                    if (rangedSearchResultEntry == null)
//                    {
//                        throw new LdapException("Directory did not return any search results during ranged retrieval.");
//                    }

//                    // Validate search result was for the object queried.
//                    if (!rangedSearchResultEntry.DistinguishedName.Equals(searchResultEntry.DistinguishedName))
//                    {
//                        throw new LdapException("Directory returned an unexpected distinguished name during ranged retrieval.");
//                    }

//                    // Validate attribute data was returned.
//                    if (rangedSearchResultEntry.Attributes?.Values == null)
//                    {
//                        throw new LdapException("Directory did not return any attribute types during ranged retrieval.");
//                    }

//                    if (rangedSearchResultEntry.Attributes.Values.Count != 1)
//                    {
//                        throw new LdapException("Directory returned invalid data during ranged retrieval.");
//                    }

//                    DirectoryAttribute[] rangedDirectoryAttributes = new DirectoryAttribute[rangedSearchResultEntry.Attributes.Values.Count];
//                    rangedSearchResultEntry.Attributes.Values.CopyTo(rangedDirectoryAttributes, 0);

//                    DirectoryAttribute rangedDirectoryAttribute = rangedDirectoryAttributes[0];

//                    string attributeTypeTmp;
//                    this.ParseRange(rangedDirectoryAttribute.Name, out attributeTypeTmp, out start, out end);

//                    // Validate attribute type was for the type queried.
//                    if (!attributeTypeTmp.Equals(attributeTypeActual))
//                    {
//                        throw new LdapException("Directory returned an unexpected distinguished name during ranged retrieval.");
//                    }

//                    valuesString.AddRange((string[])rangedDirectoryAttribute.GetValues(typeof(string)));
//                    valuesByte.AddRange((byte[][])rangedDirectoryAttribute.GetValues(typeof(byte[])));
//                } while (end != 0);
//            }

//            // Create LDAP attributes.
//            foreach (KeyValuePair<string, Tuple<List<string>, List<byte[]>>> rawAttribute in rawAttributes)
//            {
//                LdapAttribute ldapAttribute = new LdapAttribute(rawAttribute.Key, rawAttribute.Value.Item1, rawAttribute.Value.Item2);
//                ldapAttributes.Add(rawAttribute.Key, ldapAttribute);
//            }

//            return ldapAttributes;
//        }

//        /// <summary>
//        /// Paged search implementation.
//        /// </summary>
//        /// <param name="searchRequest">The configured search request.</param>
//        /// <returns>Yields the results as a SearchResultEntry.</returns>
//        private IEnumerable<SearchResultEntry> SearchPaged(SearchRequest searchRequest)
//        {
//            // Configure paging.
//            PageResultRequestControl pageResultRequestControl = new PageResultRequestControl(this.pageSize);
//            searchRequest.Controls.Add(pageResultRequestControl);

//            while (true)
//            {
//                SearchResponse searchResponse = (SearchResponse)this.ldapConnection.SendRequest(searchRequest);

//                if (searchResponse == null)
//                {
//                    break;
//                }

//                PageResultResponseControl pageResponse;
//                if (searchResponse.Controls.Length != 1
//                    || (pageResponse = searchResponse.Controls[0] as PageResultResponseControl) == null)
//                {
//                    throw new LdapException("Directory server returned an invalid page response.");
//                }

//                foreach (SearchResultEntry entry in searchResponse.Entries)
//                {
//                    yield return entry;
//                }

//                if (pageResponse.Cookie.Length == 0)
//                {
//                    break;
//                }

//                pageResultRequestControl.Cookie = pageResponse.Cookie;
//            }
//        }

//        /// <summary>
//        /// Non paged search implementation.
//        /// </summary>
//        /// <param name="searchRequest">The configured search request.</param>
//        /// <returns>Yields the results as a SearchResultEntry.</returns>
//        private IEnumerable<SearchResultEntry> SearchSimple(SearchRequest searchRequest)
//        {
//            SearchResponse searchResponse = (SearchResponse)this.ldapConnection.SendRequest(searchRequest);

//            if (searchResponse == null)
//            {
//                yield break;
//            }

//            foreach (SearchResultEntry entry in searchResponse.Entries)
//            {
//                yield return entry;
//            }
//        }
//    }

//}
