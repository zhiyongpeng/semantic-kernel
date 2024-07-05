// Copyright (c) Microsoft. All rights reserved.

using AIProxy;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.SemanticKernel;

namespace GettingStarted;

public sealed class Step5_Chat_Prompt(ITestOutputHelper output) : BaseTest(output)
{
    /// <summary>
    /// Show how to construct a chat prompt and invoke it.
    /// </summary>
    [Fact]
    public async Task RunAsync()
    {
        // Create a kernel with OpenAI chat completion
        IKernelBuilder kernelBuilder = Kernel.CreateBuilder()
            .AddOpenAIChatCompletion(
                modelId: TestConfiguration.OpenAI.ChatModelId,
                apiKey: TestConfiguration.OpenAI.ApiKey);

        kernelBuilder.Services.ConfigureHttpClientDefaults(b =>
        {
            b.ConfigurePrimaryHttpMessageHandler(() => new ZhipuAIRedirectingHandler(new HttpClientHandler()));
        });

        Kernel kernel = kernelBuilder.Build();

        // Invoke the kernel with a chat prompt and display the result
        string chatPrompt = """
            <message role="user">What is Seattle?</message>
            <message role="system">Respond with JSON.</message>
            """;

        Console.WriteLine(await kernel.InvokePromptAsync(chatPrompt));
    }
}
