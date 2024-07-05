// Copyright (c) Microsoft. All rights reserved.

using AIProxy;
using Microsoft.SemanticKernel;

namespace PromptTemplates;

/// <summary>
/// This example shows how to use GPT3.5 Chat model for prompts and prompt functions.
/// </summary>
public class PromptFunctionsWithChatGPT(ITestOutputHelper output) : BaseTest(output)
{
    [Fact]
    public async Task RunAsync()
    {
        Console.WriteLine("======== Using Chat GPT model for text generation ========");

        Kernel kernel = Kernel.CreateBuilder()
            .AddZhipuAIProxyChatCompletion(
                apiKey: TestConfiguration.ZhipuAI.ApiKey,
                modelId: TestConfiguration.ZhipuAI.ChatModelId)
            .Build();

        var func = kernel.CreateFunctionFromPrompt(
            "List the two planets closest to '{{$input}}', excluding moons, using bullet points.");

        var result = await func.InvokeAsync(kernel, new() { ["input"] = "Jupiter" });
        Console.WriteLine(result.GetValue<string>());

        /*
        Output:
           - Saturn
           - Uranus
        */
    }
}
