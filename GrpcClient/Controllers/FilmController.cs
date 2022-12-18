using Grpc.Core;
using Grpc.Net.Client;
using Microsoft.AspNetCore.Mvc;

namespace GrpcClient.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FilmController : ControllerBase
{
    private readonly FilmProtoService.FilmProtoServiceClient client;

    public FilmController(IConfiguration configuration)
    {
        var channel = GrpcChannel.ForAddress(configuration.GetValue<string>("GrpcService"));
        client = new FilmProtoService.FilmProtoServiceClient(channel);
    }

    [HttpGet("{id}")]
    public FilmModel GetFilmById(int id)
    {
        return client.GetFilm(new GetFilmRequest { FilmId = id });
    }

    [HttpGet]
    public async IAsyncEnumerable<FilmModel> GetAllFilm()
    {
        var streamAllFilms = client.GetAllFilms(new GetAllFilmsRequest());
        await foreach (var data in streamAllFilms.ResponseStream.ReadAllAsync())
        {
            yield return data;
        }
    }

    [HttpPost]
    public FilmModel AddFilm(AddFilmRequest addFilmRequest)
    {
        return client.AddFilm(addFilmRequest);
    }

    [HttpDelete]
    public DeleteFilmResponse DeleteFilm(DeleteFilmRequest deleteFilmRequest)
    {
        return client.DeleteFilm(deleteFilmRequest);
    }
}
