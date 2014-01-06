### Introduction

This repository contains a .NET assembly implementing the "Authentication and Security Layer" (SASL)
framework. SASL specifies a protocol for authentication and optional establishment of a security
layer between client and server applications and is used by internet protocols such as IMAP, POP3,
SMTP, XMPP and others.


### Usage & Examples

To use the library add the S22.Sasl.dll assembly to your project references in Visual Studio. Here's
a simple example which instantiates a new instance of the Digest-Md5 authentication mechanism and
demonstrates how it can be used to perform authentication.

	using System;
	using S22.Sasl;

	namespace Test {
		class Program {
			static void Main(string[] args) {
				SaslMechanism m = SaslFactory.Create("Digest-Md5");

				// Add properties needed by authentication mechanism.
				m.Properties.Add("Username", "Foo");
				m.Properties.Add("Password", "Bar");

				while(!m.IsCompleted)
				{
					byte[] serverChallenge = GetDataFromServer(...);
					byte[] clientResponse = m.ComputeResponse(serverChallenge);

					SendMyDataToServer(clientResponse);
				}
			}
		}
	}


### Features

The library supports the following authentication mechanisms:
 * Plain
 * Cram-Md5
 * NTLM
 * NTLMv2
 * OAuth
 * OAuth 2.0
 * Digest-Md5
 * Scram-Sha-1
 * SRP

Custom SASL Security Providers can be implemented through a simple plugin
mechanism.


### Credits

This library is copyright © 2013-2014 Torben Könke.



### License

This library is released under the [MIT license](https://github.com/smiley22/S22.Sasl/blob/master/License.md).


### Bug reports

Please send your bug reports to [smileytwentytwo@gmail.com](mailto:smileytwentytwo@gmail.com) or create a new
issue on the GitHub project homepage.
