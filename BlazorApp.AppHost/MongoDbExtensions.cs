// =======================================================
// Copyright (c) 2025. All rights reserved.
// File Name :     MongoDbExtensions.cs
// Company :       mpaulosky
// Author :        Matthew
// Solution Name : MyBlogApp
// Project Name :  AppHost
// =======================================================

using static BlazorApp.Shared.Services;

namespace AppHost;

/// <summary>
///   Extension methods for adding and configuring MongoDB resources with Aspire 9.4.0 features.
/// </summary>
public static class MongoDbExtensions
{

	/// <summary>
	///   Adds MongoDB services to the distributed application builder, including resource tagging, grouping, and improved
	///   seeding logic.
	/// </summary>
	/// <param name="builder">The distributed application builder.</param>
	/// <returns>The MongoDB database resource builder.</returns>
	public static IResourceBuilder<MongoDBDatabaseResource> AddMongoDbServices(
			this IDistributedApplicationBuilder builder)
	{

		var mongoDbConnection = builder.AddParameter("mongoDb-connection", secret: true);

		var database = builder.AddMongoDB(SERVER)
				.WithLifetime(ContainerLifetime.Persistent)
				.WithDataVolume($"{SERVER}-data", isReadOnly: false)
				.WithMongoExpress()
				.WithEnvironment("MONGODB-CONNECTION", mongoDbConnection)
				.AddDatabase(DATABASE);

		// Return the database resource builder so health checks are included
		return database;
	}

}