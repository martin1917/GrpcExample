using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using GrpcExample.Data;
using GrpcExample.Models;
using Microsoft.EntityFrameworkCore;

namespace GrpcExample.Services;

public class FilmService : FilmProtoService.FilmProtoServiceBase
{
	private readonly ApplicationDbContext ctx;

	public FilmService(ApplicationDbContext ctx)
	{
		this.ctx = ctx;
	}

	public override async Task<FilmModel> AddFilm(AddFilmRequest request, ServerCallContext context)
	{
		var film = new Film
		{
			Name = request.Film.Name,
			Description = request.Film.Description,
			ReleaseDate = request.Film.ReleaseDate.ToDateTime()
		};

		ctx.Entry(film).State = EntityState.Added;
		await ctx.SaveChangesAsync();

		return new FilmModel
		{
			Id = film.Id,
            Name = film.Name,
            Description = film.Description,
            ReleaseDate = Timestamp.FromDateTimeOffset(film.ReleaseDate)
        };
	}

	public override async Task<DeleteFilmResponse> DeleteFilm(DeleteFilmRequest request, ServerCallContext context)
	{
		var film = await ctx.Films.FirstOrDefaultAsync(f => f.Id == request.FilmId);
		if (film == null)
		{
            throw new RpcException(new Status(StatusCode.NotFound, $"Product with ID={request.FilmId} is not found."));
        }

		ctx.Films.Remove(film);
		var deleteCount = await ctx.SaveChangesAsync();

		return new DeleteFilmResponse
		{
			Success = deleteCount > 0,
		};
    }

	public override async Task GetAllFilms(GetAllFilmsRequest request, IServerStreamWriter<FilmModel> responseStream, ServerCallContext context)
	{
		var films = await ctx.Films.ToListAsync();
		
		foreach (var film in films)
		{
			var filmModel = new FilmModel
			{
                Id = film.Id,
                Name = film.Name,
                Description = film.Description,
                ReleaseDate = Timestamp.FromDateTimeOffset(film.ReleaseDate)
            };

			await responseStream.WriteAsync(filmModel);
        }
	}

	public override async Task<FilmModel> GetFilm(GetFilmRequest request, ServerCallContext context)
	{
		var film = await ctx.Films.FirstOrDefaultAsync(f => f.Id == request.FilmId);
		if (film == null)
		{
			throw new RpcException(new Status(StatusCode.NotFound, $"Film with ID={request.FilmId} is not found."));
		}

		var filmModel = new FilmModel
		{
			Id = film.Id,
			Name = film.Name,
			Description = film.Description,
			ReleaseDate = Timestamp.FromDateTimeOffset(film.ReleaseDate)
		};

		return filmModel;
	}
}
