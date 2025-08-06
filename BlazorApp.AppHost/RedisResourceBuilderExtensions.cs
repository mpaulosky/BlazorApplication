// =======================================================
// Copyright (c) 2025. All rights reserved.
// File Name :     RedisResourceBuilderExtensions.cs
// Company :       mpaulosky
// Author :        Matthew
// Solution Name : MyBlogApp
// Project Name :  AppHost
// =======================================================


namespace AppHost;

internal static class RedisResourceBuilderExtensions
{

	public static IResourceBuilder<RedisResource> WithClearCommand(
			this IResourceBuilder<RedisResource> builder)
	{
		builder.WithCommand(
				name:
				"clear-cache",
				displayName:
				"Clear Cache",
				executeCommand:
				context => OnRunClearCacheCommandAsync(builder, context),
				commandOptions: new CommandOptions
				{
					IconName = "AnimalRabbitOff",
					IconVariant = IconVariant.Filled,
					UpdateState = OnUpdateResourceState,
					ConfirmationMessage = "Are you sure you want to clear the cache?",
					Description = "This command will clear all cached data in the Redis cache.",
				}
		);

		return builder;

	}

	private static async Task<ExecuteCommandResult> OnRunClearCacheCommandAsync(
			IResourceBuilder<RedisResource> builder,
			ExecuteCommandContext context)
	{

		var connectionString = await builder.Resource.GetConnectionStringAsync() ??
													throw new InvalidOperationException(
															$"Unable to get the '{context.ResourceName}' connection string.");

		await using var connection = await ConnectionMultiplexer.ConnectAsync(connectionString);

		var database = connection.GetDatabase();

		await database.ExecuteAsync("FLUSHALL");

		return CommandResults.Success();

	}
	private static ResourceCommandState OnUpdateResourceState(
			UpdateCommandStateContext context)
	{

		var logger = context.ServiceProvider.GetRequiredService<ILogger<Program>>();

		if (logger.IsEnabled(LogLevel.Information))
		{
			logger.LogInformation("Updating resource state: {ResourceSnapshot}", context.ResourceSnapshot);
		}

		return context.ResourceSnapshot.HealthStatus is HealthStatus.Healthy
			? ResourceCommandState.Enabled
			: ResourceCommandState.Disabled;

	}

}