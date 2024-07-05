// Copyright (c) Microsoft. All rights reserved.

using Microsoft.Extensions.DependencyInjection;
using Microsoft.SemanticKernel;
namespace AIProxy;

/// <summary>
/// 智谱AI服务集合扩展
/// </summary>
public static class AIProxyServiceCollectionExtensions
{
    /// <summary>
    /// Adds the ZhipuAI chat completion service to the list.
    /// </summary>
    /// <param name="builder">The <see cref="IKernelBuilder"/> instance to augment.</param>
    /// <param name="modelId">OpenAI model name, see https://platform.openai.com/docs/models</param>
    /// <param name="apiKey">OpenAI API key, see https://platform.openai.com/account/api-keys</param>
    /// <param name="orgId">OpenAI organization id. This is usually optional unless your account belongs to multiple organizations.</param>
    /// <param name="serviceId">A local identifier for the given AI service</param>
    /// <param name="httpClient">The HttpClient to use with this service.</param>
    public static IKernelBuilder AddZhipuAIProxyChatCompletion(
        this IKernelBuilder builder,
        string modelId,
        string apiKey,
        string? orgId = null,
        string? serviceId = null,
        HttpClient? httpClient = null)
    {
        builder.AddOpenAIChatCompletion(modelId, apiKey, orgId, serviceId, httpClient);
        builder.Services.ConfigureHttpClientDefaults(
            b => b.ConfigurePrimaryHttpMessageHandler(() => new ZhipuAIRedirectingHandler(new HttpClientHandler())));

        return builder;
    }

    /// <summary>
    /// Adds the OpenAI chat completion service to the list.
    /// </summary>
    /// <param name="builder">The <see cref="IKernelBuilder"/> instance to augment.</param>
    /// <param name="modelId">OpenAI model name, see https://platform.openai.com/docs/models</param>
    /// <param name="apiKey">OpenAI API key, see https://platform.openai.com/account/api-keys</param>
    /// <param name="orgId">OpenAI organization id. This is usually optional unless your account belongs to multiple organizations.</param>
    /// <param name="serviceId">A local identifier for the given AI service</param>
    /// <param name="httpClient">The HttpClient to use with this service.</param>
    public static IKernelBuilder AddOpenAIProxyChatCompletion(
        this IKernelBuilder builder,
        string modelId,
        string apiKey,
        string? orgId = null,
        string? serviceId = null,
        HttpClient? httpClient = null)
    {
        builder.AddOpenAIChatCompletion(modelId, apiKey, orgId, serviceId, httpClient);
        builder.Services.ConfigureHttpClientDefaults(
            b => b.ConfigurePrimaryHttpMessageHandler(() => new OpenAIRedirectingHandler(new HttpClientHandler())));

        return builder;
    }
}
