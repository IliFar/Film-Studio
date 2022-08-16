# Film-Studio
## Asp.net core WebApi with front-end at wwwroot folder

**HowToStart**

1. Start IIExpress server in Visual Studio => Will directly take you to the front-end at wwwroot.

**HowToUSe**

1. Register an admin is at endpoint api/user/signup
2. Register a user is at endpoint api/filmstudio/register
3. Get all films is at api/films (get method)
4. Get film by id is at api/films/{id} (get method with id)
5. Add Film is at api/films (put method) => add film without copies and then add copies to each film. Copies have their own controller.
6. Update film is at api/films (post method)
7. Rent a film is at api/films/rent[queryString] (post method)
