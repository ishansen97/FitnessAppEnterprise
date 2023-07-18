using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace ReportService.Services
{
  public class RemoteService
  {
    private readonly IHttpContextAccessor _contextAccessor;
    private readonly IHttpClientFactory _clientFactory;

    protected RemoteService(
      IConfiguration configuration,
      IHttpContextAccessor contextAccessor,
      IHttpClientFactory clientFactory)
    {
      Configuration = configuration;
      _contextAccessor = contextAccessor;
      _clientFactory = clientFactory;
    }

    public IConfiguration Configuration { get; }

    public HttpClient HttpClient => _clientFactory.CreateClient();

    public HttpContext HttpContext => _contextAccessor.HttpContext;

    public IHttpClientFactory HttpClientFactory => _clientFactory;
  }
}
