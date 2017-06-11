using System;
using System.Collections.Generic;
using System.Configuration;

namespace S22.Sasl {
	/// <summary>
	/// A factory class for producing instances of Sasl mechanisms.
	/// </summary>
	public static class SaslFactory {
		/// <summary>
		/// A dictionary of Sasl mechanisms registered with the factory class.
		/// </summary>
		static Dictionary<string, Type> mechanisms {
			get;
			set;
		}

        /// <summary>
        /// A list of the names of all available mechanisms.
        /// </summary>
        public static IEnumerable<string> Mechanisms {
            get {
                return mechanisms.Keys;
            }
        }

		/// <summary>
		/// Creates an instance of the Sasl mechanism with the specified
		/// name.
		/// </summary>
		/// <param name="name">The name of the Sasl mechanism of which an
		/// instance will be created.</param>
		/// <returns>An instance of the Sasl mechanism with the specified name.</returns>
		/// <exception cref="ArgumentNullException">The name parameter is null.</exception>
		/// <exception cref="SaslException">A Sasl mechanism with the
		/// specified name is not registered with Sasl.SaslFactory.</exception>
		public static SaslMechanism Create(string name) {
			name.ThrowIfNull("name");
			if (!mechanisms.ContainsKey(name)) {
				throw new SaslException("A Sasl mechanism with the specified name " +
					"is not registered with Sasl.SaslFactory.");
			}
			Type t = mechanisms[name];
			object o = Activator.CreateInstance(t, true);
			return o as SaslMechanism;
		}

		/// <summary>
		/// Registers a Sasl mechanism with the factory using the specified name.
		/// </summary>
		/// <param name="name">The name with which to register the Sasl mechanism
		/// with the factory class.</param>
		/// <param name="t">The type of the class implementing the Sasl mechanism.
		/// The implementing class must be a subclass of Sasl.SaslMechanism.</param>
		/// <exception cref="ArgumentNullException">The name parameter or the t
		/// parameter is null.</exception>
		/// <exception cref="ArgumentException">The class represented by the
		/// specified type does not derive from Sasl.SaslMechanism.</exception>
		/// <exception cref="SaslException">The Sasl mechanism could not be
		/// registered with the factory. Refer to the inner exception for error
		/// details.</exception>
		public static void Add(string name, Type t) {
			name.ThrowIfNull("name");
			t.ThrowIfNull("t");
			if (!t.IsSubclassOf(typeof(SaslMechanism))) {
				throw new ArgumentException("The type t must be a subclass " +
					"of Sasl.SaslMechanism");
			}
			try {
                mechanisms.Add(name, t);
			} catch (Exception e) {
				throw new SaslException("Registration of Sasl mechanism failed.", e);
			}
		}

		/// <summary>
		/// Static class constructor. Initializes static properties.
		/// </summary>
		static SaslFactory() {
            mechanisms = new Dictionary<string, Type>(
                StringComparer.InvariantCultureIgnoreCase) {
                { "Plain", typeof(Mechanisms.SaslPlain) },
                { "Cram-Md5", typeof(Mechanisms.SaslCramMd5) },
                { "Digest-Md5", typeof(Mechanisms.SaslDigestMd5) },
                { "Scram-Sha-1", typeof(Mechanisms.SaslScramSha1) },
                { "Ntlm", typeof(Mechanisms.SaslNtlm) },
                { "Ntlmv2", typeof(Mechanisms.SaslNtlmv2) },
                { "OAuth", typeof(Mechanisms.SaslOAuth) },
                { "OAuth2", typeof(Mechanisms.SaslOAuth2) },
                { "Srp", typeof(Mechanisms.SaslSrp) }
            };            
            // Register any custom mechanisms configured in the app.config.
            var configSection = ConfigurationManager.GetSection("saslConfigSection")
                as SaslConfigurationSection;
            if (configSection != null) {                
                foreach(SaslProvider provider in configSection.SaslProviders) {
                    mechanisms.Add(provider.Name, Type.GetType(provider.Type));
                }
            }
		}
	}
}
