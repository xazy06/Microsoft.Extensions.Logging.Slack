using Microsoft.AspNetCore.Hosting;
using System;
using System.Net.Http;

namespace Microsoft.Extensions.Logging.Slack
{
	public static class SlackLoggerExtension
	{
		public static ILoggingBuilder AddSlack(this ILoggingBuilder builder, SlackConfiguration configuration, string applicationName, string environmentName, HttpClient client = null)
		{
			if (string.IsNullOrEmpty(applicationName))
			{
				throw new ArgumentNullException(nameof(applicationName));
			}

			if (string.IsNullOrEmpty(environmentName))
			{
				throw new ArgumentNullException(nameof(environmentName));
			}

			ILoggerProvider provider = new SlackLoggerProvider((n,l,e) => l >= configuration.MinLevel, configuration, client, applicationName, environmentName);

            builder.AddProvider(provider);

			return builder;
		}

		public static ILoggingBuilder AddSlack(this ILoggingBuilder builder, Func<string, LogLevel, Exception, bool> filter, SlackConfiguration configuration, string applicationName, string environmentName, HttpClient client = null)
		{
			if (string.IsNullOrEmpty(applicationName))
			{
				throw new ArgumentNullException(nameof(applicationName));
			}

			if (string.IsNullOrEmpty(environmentName))
			{
				throw new ArgumentNullException(nameof(environmentName));
			}

			ILoggerProvider provider = new SlackLoggerProvider(filter,configuration, client, applicationName, environmentName);

			builder.AddProvider(provider);

			return builder;
		}

		public static ILoggingBuilder AddSlack(this ILoggingBuilder builder,  SlackConfiguration configuration, IHostingEnvironment hostingEnvironment, HttpClient client = null)
		{
			ILoggerProvider provider = new SlackLoggerProvider((n, l,e) => l >= configuration.MinLevel, configuration, client, hostingEnvironment.ApplicationName, hostingEnvironment.EnvironmentName);

			builder.AddProvider(provider);

			return builder;
		}

		public static ILoggingBuilder AddSlack(this ILoggingBuilder builder, Func<string, LogLevel, Exception, bool> filter, SlackConfiguration configuration, IHostingEnvironment hostingEnvironment, HttpClient client = null)
		{
			ILoggerProvider provider = new SlackLoggerProvider(filter, configuration, client, hostingEnvironment.ApplicationName, hostingEnvironment.EnvironmentName);

			builder.AddProvider(provider);

			return builder;
		}
	}
}