using System.Configuration;

namespace S22.Sasl {
	/// <summary>
	/// Represents the sasl section within a configuration
	/// file.
	/// </summary>
	public class SaslConfigurationSection : ConfigurationSection {
		/// <summary>
		/// The saslProviders section contains a collection of
		/// saslProvider elements.
		/// </summary>
		[ConfigurationProperty("saslProviders", IsRequired = false,
			IsKey = false, IsDefaultCollection = true)]
		public SaslProviderCollection SaslProviders {
			get {
				return ((SaslProviderCollection) base["saslProviders"]);
			}
			set {
				base["saslProviders"] = value;
			}
		}
	}

	/// <summary>
	/// Represents a saslProvider configuration element within the
	/// saslProviders section of a configuration file.
	/// </summary>
	[ConfigurationCollection(typeof(SaslProvider),
		CollectionType=ConfigurationElementCollectionType.BasicMapAlternate)]
	public class SaslProviderCollection : ConfigurationElementCollection {
		/// <summary>
		/// The name of the configuration element.
		/// </summary>
		internal const string PropertyName = "saslProvider";

		/// <summary>
		/// Gets the name used to identify this collection of elements
		/// in the configuration file.
		/// </summary>
		protected override string ElementName {
			get { return PropertyName; }
		}

		/// <summary>
		/// Returns the SaslProvider instance for the saslProvider
		/// element with the specified name.
		/// </summary>
		/// <param name="name">The name of the saslProvider element to
		/// retrieve.</param>
		/// <returns>The SaslProvider instance with the specified name
		/// or null.</returns>
		public new SaslProvider this[string name] {
			get {
				return (SaslProvider) BaseGet(name);
			}
		}

		/// <summary>
		/// Returns the SaslProvider instance for the saslProvider
		/// element at the specified index.
		/// </summary>
		/// <param name="index">The index of the saslProvider element
		/// to retrieve.</param>
		/// <returns>The SaslProvider instance with the specified
		/// index.</returns>
		/// <exception cref="ConfigurationErrorsException">Thrown if the
		/// index is less than 0 or if there is no SaslProvider instance
		/// at the specified index.</exception>
		public SaslProvider this[int index] {
			get {
				return (SaslProvider) BaseGet(index);
			}
		}

		/// <summary>
		/// Gets the collection type of the SaslProviderCollection.
		/// </summary>
		public override ConfigurationElementCollectionType CollectionType {
			get {
				return ConfigurationElementCollectionType.BasicMapAlternate;
			}
		}  
  
		/// <summary>
		/// Indicates whether the specified System.Configuration.ConfigurationElement
		/// exists in the SaslProviderCollection.
		/// </summary>
		/// <param name="elementName">The name of the element to verify.</param>
		/// <returns>Returns true if the element exists in the collection,
		/// otherwise false.</returns>
		protected override bool IsElementName(string elementName) {
			return elementName == PropertyName;
		}  

		/// <summary>
		/// Creates a new instance of the SaslProvider class.
		/// </summary>
		/// <returns>A new instance of the SaslProvider class.</returns>
		protected override ConfigurationElement CreateNewElement() {
			return new SaslProvider();
		}

		/// <summary>
		/// Gets the element key for the specified SaslProvider element.
		/// </summary>
		/// <param name="element">A SaslProvider element to retrieve the
		/// element key for.</param>
		/// <returns>The unique element key of the specified SaslProvider
		/// instance.</returns>
		protected override object GetElementKey(ConfigurationElement element) {
			return ((SaslProvider) element).Name;
		}
	}

	/// <summary>
	/// Represents a saslProvider section within the saslProviders
	/// section of a configuration file.
	/// </summary>
	public class SaslProvider : ConfigurationSection {
		/// <summary>
		/// The name of the saslProvider. This attribute must be unique in
		/// that no two saslProvider elements exists that have the same
		/// name attribute.
		/// </summary>
		[ConfigurationProperty("name", IsRequired = true)]
		public string Name {
			get {
				return (string) base["name"];
			}
			set {
				base["name"] = value;
			}
		}

		/// <summary>
		/// The type name of the SaslMechanism exposed by the
		/// saslProvider.
		/// </summary>
		[ConfigurationProperty("type", IsRequired = true)]
		public string Type {
			get {
				return (string) base["type"];
			}
			set {
				base["type"] = value;
			}
		}

		/// <summary>
		/// Retrieves the setting with the specified name for this saslProvider.
		/// </summary>
		/// <param name="name">The name of the setting to retrieve.</param>
		/// <returns>The value of the setting with the specified name or null
		/// if the setting could not be found.</returns>
		public new string this[string name] {
			get {
				if (Settings[name] != null)
					return Settings[name].Value;
				return null;
			}
		}

		/// <summary>
		/// Represents a collection of arbitrary name-value pairs which can be
		/// added to the saslProvider element.
		/// </summary>
		[ConfigurationProperty("", IsDefaultCollection = true)]
		public NameValueConfigurationCollection Settings {
			get {
				return (NameValueConfigurationCollection) base[""];
			}
		}
	}
}
