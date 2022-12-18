using GrpcExample.Models;

namespace GrpcExample.Data;

public class ContextSeed
{
    public static void Seed(ApplicationDbContext ctx)
    {
        if(!ctx.Films.Any())
        {
            var films = new List<Film>()
            {
                new Film
                {
                    Id = 1,
                    Name = "Fall",
                    Description = "Two friends get stuck on an abandoned 600-meter tower in the desert. A dizzying thriller about survival",
                    ReleaseDate = new DateTime(2022, 8, 10)
                },

                new Film
                {
                    Id = 2,
                    Name = "Uncharted 2022",
                    Description = 
                        "Nathan Drake and Victor \"Sally\" Sullivan, two adventurers, go in search of the world's greatest treasure. " +
                        "In addition, they hope to find clues that will lead them to Nathan's long-lost brother",
                    ReleaseDate = new DateTime(2022, 2, 8)
                },
                
                new Film
                {
                    Id = 3,
                    Name = "Doctor Strange in the Multiverse of Madness",
                    Description = "Continuation of the magical adventures of Doctor Strange in new mystical worlds and in confrontation with new enemies",
                    ReleaseDate = new DateTime(2022, 5, 2)
                },
                
                new Film
                {
                    Id = 4,
                    Name = "The Batman 2022",
                    Description = 
                        "After two years of searching for justice on the streets of Gotham, Batman becomes the personification " +
                        "of merciless retribution for the citizens. When a series of violent attacks on high-ranking officials" +
                        " takes place in the city, evidence leads Bruce Wayne into the darkest corners of the underworld, where he" +
                        " meets Catwoman, Penguin, Carmine Falcone and the Riddler. Now Batman himself is under the gun, who will have " +
                        "to distinguish friend from enemy and restore justice in the name of Gotham",
                    ReleaseDate = new DateTime(2022, 3, 1)
                },
                
                new Film
                {
                    Id = 5,
                    Name = "Thor: Love and Thunder",
                    Description = "Jane Foster takes on the duties of the Thunder God and becomes the owner of the hammer Mjolnir",
                    ReleaseDate = new DateTime(2022, 6, 23)
                },
            };
            ctx.AddRange(films);
            ctx.SaveChanges();
        }
    }
}
