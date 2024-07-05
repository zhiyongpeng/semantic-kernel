namespace AIProxy;

/// <summary>
/// OpenAI 重定向处理程序
/// </summary>
public sealed class OpenAIRedirectingHandler : DelegatingHandler
{
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="innerHandler"></param>
    public OpenAIRedirectingHandler(HttpMessageHandler innerHandler)
        : base(innerHandler)
    { }

    /// <summary>
    /// 发送 HTTP 请求到内部处理程序以发送到服务器。
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    protected override Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request, CancellationToken cancellationToken)
    {
        if (request.RequestUri!.LocalPath == "/v1/chat/completions")
        {
            request.RequestUri = new UriBuilder(request.RequestUri!)
            {
                Host = "oai.servicex.cc"
            }.Uri;
        }

        return base.SendAsync(request, cancellationToken);
    }
}
