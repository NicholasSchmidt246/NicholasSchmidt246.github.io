## Professional self-assessment
-- Professional self-assessment to go here --

# Sudoku Puzzle Solver
## Artifact Origin
The artifact that was chosen to demonstrate my skills in Software Design and Engineering, Algorithms and Data Structure, as well as Databases is an open source Sudoku Puzzle Solver in node.js. The node.js source for this artifact was created in 2011 and can be found at [https://github.com/dachev/sudoku](https://github.com/dachev/sudoku). 

## Code Review
-- Code review video --

## Enhancement 1: *(Software design and engineering)*
### Rewrite the code in C# and transform it into a web service with a defined API. 

Sample: Get Moves
``` C#
[HttpGet(Name = "Count")]
public async Task<int> Get([FromHeader] Guid playerId, [FromHeader] Guid puzzleId)
{
    // TODO: Impliment
    await Task.Delay(0);
    Response.StatusCode = 501; // Http 501 means not Implimented 
    return int.MinValue;
}
```
Source: [MovesController](https://github.com/NicholasSchmidt246/NicholasSchmidt246.github.io/blob/master/Sudoku%20C%23%20WebService/Sudoku%20WebService/Sudoku%20WebService/Controllers/MovesController.cs)

Sample: Get Players
``` C#
[HttpGet(Name = "Info")]
public async Task<string> Get([FromHeader] Guid id)
{
    // TODO: Impliment
    await Task.Delay(0);
    Response.StatusCode = 501; // Http 501 means not implimented
    return string.Empty;
}
```
Source: [PlayersController](https://github.com/NicholasSchmidt246/NicholasSchmidt246.github.io/blob/master/Sudoku%20C%23%20WebService/Sudoku%20WebService/Sudoku%20WebService/Controllers/PlayersController.cs)

Sample: Get Puzzles
``` C#
[HttpGet(Name = "Existing")]
public async Task Get([FromHeader] Guid userId, [FromHeader] Guid id)
{
    using (var Auth = new AuthorizationStrategy(userId))
    {
        if(!await Auth.IsAuthorized(userId))
        {
            Response.StatusCode = 403; // Http 403 means Forbidden, which is the actual message you want to send to an unauthorized individual. Http 401 unauthorized (despite it's name) is used for unauthenticated
        }
    }

    using var Play = new PlayStrategy(userId, id);
    string ContentType = Request.Headers["Content-Type"]; 
    Response.Body = await Play.GetPuzzle(ContentType);
}
```
Source: [PuzzlesController](https://github.com/NicholasSchmidt246/NicholasSchmidt246.github.io/blob/master/Sudoku%20C%23%20WebService/Sudoku%20WebService/Sudoku%20WebService/Controllers/PuzzlesController.cs)

Sample: Get Solutions
``` C#
[HttpGet]
public async Task<string> Get([FromHeader] Guid userId, [FromHeader] Guid puzzleId, [FromHeader] bool full)
{
    // TODO: Impliment
    await Task.Delay(0);
    Response.StatusCode = 501; // Http 501 means not implimented
    return string.Empty;
}
```
Source: [SolutionsController](https://github.com/NicholasSchmidt246/NicholasSchmidt246.github.io/blob/master/Sudoku%20C%23%20WebService/Sudoku%20WebService/Sudoku%20WebService/Controllers/SolutionsController.cs)

### Software design and engineering narrative
This artifact is a perfect addition to my ePortfolio as it demonstrates an ability to review a product and securely deploy it to the cloud, which is something I have had to do for work over the last few years and is a highly sought after skill in the current job market. Personally it has the added touch of completing a full circle of the first final project I ever had in my college career, which was to build a Sudoku puzzle solver using linked lists in c++. The improvements made to this project that best highlight my skills in Software Design and Engineering is the addition of a RESTful API to it. I was able to design and create RESTful micro-service endpoints for the playing of Sudoku puzzles and the attached Sudoku puzzle solver itself. While the work is not yet fully complete, the design will show a service which used to run on a webpage now securely behind an API with authorization requirements. While this may not be incredibly beneficial in a Sudoku puzzle solver, it should be sufficient to demonstrate a highly marketable skillset to employers seeking to bring their code bases into the cloud. I believe I am well on my way to demonstrating more course objectives at this point than I had originally planned. In fact at this point I feel that in my API enhancement alone I am demonstrating the following course objectives:

1.	Design, develop, and deliver professional-quality oral, written, and visual communications that are coherent, technically sound, and appropriately adapted to specific audiences and contexts.
2.	Demonstrate an ability to use well-founded and innovative techniques, skills, and tools in computing practices for the purpose of implementing computer solutions that deliver value and accomplish industry-specific goals.
3.	Develop a security mindset that anticipates adversarial exploits in software architecture and designs to expose potential vulnerabilities, mitigate design flaws, and ensure privacy and enhanced security of data and resources.

I have evidence of (1) specifically in the SudokuSolver [API document](https://github.com/NicholasSchmidt246/NicholasSchmidt246.github.io/blob/master/Sudoku%20C%23%20WebService/Sudoku%20WebService/SudokuSolver%20API.docx). I have demonstrated (2) by using some industry standard tools, some of which ironically, I learned while writing the SudokuSolver API document. I will include a list of those at the end of this document. My work creating a flexible API in .Net Core that can deploy to an Azure App Service that will utilize the environment variables present and utilize other Azure resources further exemplifies the migration to cloud aspect of my experience in industry-specific goals. By integrating a simulated authorization system and input validation for all input resources, I have shown evidence of (3) to secure the migrated solution has consideration of access to who has authorization to access certain resources and ensuring all input is valid and/or sanitized.
I have faced some major challenges during this enhancement. The original plan was to port the code from node.js, but the code ended up being near illegible among many other discovered issues. So instead I have repaired the discovered design flaw, and am rewriting the entire tool, with all of the planned enhancements. Since I used the same artifact for each aspect, it ended up meaning that each piece is getting worked on a bit in tandem. It also means that for as much work as I have done designing, developing, and testing, I do not have a completed artifact to show at this time. As mentioned above I was also able to learn about some industry standards I was not familiar with, despite having works specifically in this field in this specialty for the last few years. I have also learned the way my company does code reviews is woefully inadequate, that I have been robbed of many learning opportunities along the way.

Industry standards learned during design / development
- The Server bans REST methods not used instead of not defining them 
  - With the exceptions of Get and Head
- Banned REST methods respond with a list of acceptable methods that can be used.
- The Options method is designed to exist for each controller to proactively define the available methods on it. 
- Designed a define-or-default system for “Content-Type” header which will define the format of a requests body.
  - Industry standard default is “application/json”
- Designed a define-of-default system for Query string parameter “alt” which defines the clients desired output format.
  - Industry standard default is “json”

## Enhancement 2: *(Algorithms and data structure)*
### Repair the design flaw and adjust the logic to function on squares other than just 9x9. 

Sample: GeneratePuzzleFromSeed
``` C#
public Dictionary<int, int?> GeneratePuzzleFromSeed(string difficulty)
{
    var board = new Dictionary<int, int?>();
    // Load starting seed
    LoadSeed(difficulty);

    // Determine Transformations
    var Rand = new Random();
    var rotations = Rand.Next(0, 4);
    var flipHoriz = Rand.Next(0, 2);
    var flipVert = Rand.Next(0, 2);

    // Execute Transformations
    while (rotations > 0)
    {
        board = RotateBoard(seedBoard);
        rotations = rotations - 1;
    }
    while (flipHoriz > 0)
    {
        board = FlipBoardHoriz(board);
        flipHoriz = flipHoriz - 1;
    }
   while (flipVert > 0)
   {
      board = FlipBoardVert(board);
      flipVert = flipVert - 1;
   }

   // Execute Token Shuffle
   ShuffleTokens();
   board = ApplyTokens(board);

   return board;
}
```
Source: [SeedTransformationStrategy](https://github.com/NicholasSchmidt246/NicholasSchmidt246.github.io/blob/master/Sudoku%20C%23%20WebService/Sudoku%20WebService/Sudoku%20WebService/Strategies/SeedTransformationStrategy.cs)

### Algorithms and data structure narrative
The artifact worked out better than I could have hoped to demonstrate my skills with Algorithms and Data Structures. While doing a code review for the Sudoku puzzle solver, I discovered a design flaw with how it generates puzzles, in that it enters values with no regard whatsoever to the integrity of the puzzle, then it shuffles them again, then removes values randomly and try to solve the puzzle after each one, and only committing to removing one of those values if the puzzle is able to be solved. This means that unless a valid completed puzzle is generated after a random shuffle, the entire process will loop. It also means that eventually when a puzzle is generated, there is no control over the difficulty of it. It occurred to me that there should be a lot of evaluation and consideration for anything that passes as a generated puzzle and that it would be wiser to use a seed. My planned enhancement was to allow dynamic sized puzzles to be played/solved, so instead of relying on magic numbers in code as the artifact did, I would rely on the relationships between those numbers to achieve this. I feel like these enhancements demonstrate the following course objective: 

1.	Design and evaluate computing solutions that solve a given problem using algorithmic principles and computer science practices and standards appropriate to its solution, while managing the trade-offs involved in design choices.
2.	Demonstrate an ability to use well-founded and innovative techniques, skills, and tools in computing practices for the purpose of implementing computer solutions that deliver value and accomplish industry-specific goals.

I have demonstrated (1) specifically with my repairing of the design flaw for generating puzzles in the artifact. The old design would generate an appropriate amount of each number, but placement was completely random and only check as each node was tested after it was removed to see if it the puzzle was usable then. The new design will take pre-generated and tested puzzles as seeds and modify them to appear as a completely new puzzle. This occurs by implementing a random flip on the horizontal access, random flip on the vertical access, random number of rotations at 90 degrees, and lastly the most effective tool replacing the displayed value with a shuffled list of possible values. To expand on that last part, if the puzzle has a size of 81 nodes, we know the square root of 81 is 9, which is the dimension of the square puzzle so our number set is { 1, 2, 3, 4, 5, 6, 7, 8, 9 } if we shuffle that set to be say { 4, 1, 9, 7, 6, 2, 3, 8, 5 } and replace the seeds values with  { 1 -> a, 2 -> b, 3 -> c … etc} and then using our shuffled number set to replace { a -> 4, b -> 1, c -> 9… etc } we may it much harder for a user to detect they are playing the same puzzle. In fact given a 9x9 puzzle using all of the above listed tools, one seed can have 5,806,080 representations. Allowing for the dynamic puzzle sizes on top of that allow us to support / solve 4x4 puzzles, which can have 384 representations per seed of 4x4 puzzles, and 334,764,638,208,000 representations for each 16x16 seed. The limitation to this design, is that in order to be able to generate a specific puzzle size, we will need a pre-generated seed. To house these objects, and reduce the amount of loops required to associate them I have created a Dictionary with the key being an integer representing a node identity, the value is an object that contains a nullable integer to represent the current value of the node and a list of integers to represent to possible values of a node. Using this pair allows me to control everything related to a node in one lookup. Using integer as a key, I am able to determine the dimension of the puzzle by calculating the square root of the size of the puzzle, the rows by integer dividing the dimension of the puzzle, the columns by calculating the modulus of the dimension, and the cells by using a bit more complicated or an algorithm using the square root of the dimension to calculate the top left node in each cell, and then similar algorithms to determine each node in the same cell. I demonstrated (2) specifically with the objects chosen to house the dynamic data and means of traversing them. 
	I did not have too many challenges with this aspect of the project, the design repair was the biggest impact to my goal, but removed the requirement for a puzzle generator for now, which made the workload manageable. The definition of the objects was a little challenging too at some points. Originally I had them as separate lists and realized the amount of room for error I was inviting by doing so. Now I can opt out of calculating possible moves for a field that has been populated, I can track the moves that have been completed and their details to identify the populated fields that can be changed, I can calculate the nodes to be looked at to determine changes to their possible values list based on a completed move, etc. Since I used the same artifact for each aspect, it ended up meaning that each piece is getting worked on a bit in tandem. It also means that for as much work as I have done designing, developing, and testing, I do not have a completed artifact to show at this time.

## Enhancement 3: *(Databases)*
### Integrate with MongoDb to hold game data / seeds. 

Sample: Read
``` C#
protected async Task<IAsyncCursor<BsonDocument>> ReadAsync(FilterDefinition<BsonDocument> filter, CancellationToken cancellationToken)
{
    var Options = new FindOptions<BsonDocument>();
    return await Collection.FindAsync(filter, Options, cancellationToken);
}
```
Source: [MongoDbAccess](https://github.com/NicholasSchmidt246/NicholasSchmidt246.github.io/blob/master/Sudoku%20C%23%20WebService/Sudoku%20WebService/Sudoku%20WebService/DataAccess/MongoDbAccess.cs)

Sample: ReadGame
``` C#
public async Task<GameModel> ReadGame(Guid id, Guid playerId, CancellationToken cancellationToken)
{
    if (!await GameExists(id, cancellationToken))
    {
        throw new ArgumentException("Unauthorized");
    }

    var Filter = new BsonDocument()
    {
        { "GameId", id.ToString() },
        { "PlayerId", playerId.ToString() }
    };

    var ReadCursorResults = await ReadAsync(Filter, cancellationToken);
    var ReadResults = await ReadCursorResults.ToListAsync(cancellationToken);

    if (ReadResults.Count != 1)
    {
      throw new Exception();
    }

    ReadResults[0].Remove("_id");

    GameModel Game = BsonSerializer.Deserialize<GameModel>(ReadResults[0].AsBsonDocument);

    return Game;
}
```
Source: [GamesDbAccess](https://github.com/NicholasSchmidt246/NicholasSchmidt246.github.io/blob/master/Sudoku%20C%23%20WebService/Sudoku%20WebService/Sudoku%20WebService/DataAccess/GamesDbAccess.cs) 

Sample: ReadPlayer
``` C#
public async Task<PlayerModel> ReadPlayer(Guid id, CancellationToken cancellationToken)
{
    if (!await PlayerExists(id, cancellationToken))
    {
        throw new ArgumentException("Unauthorized");
    }

    var Filter = Builders<BsonDocument>.Filter.Eq("PlayerId", id.ToString());

    var ReadCursorResults = await ReadAsync(Filter, cancellationToken);
    var ReadResults = await ReadCursorResults.ToListAsync(cancellationToken);

    if(ReadResults.Count != 1)
    {
        throw new Exception();
    }

    ReadResults[0].Remove("_id");

    var Player = new PlayerModel()
    {
        UserId = ReadResults[0]["PlayerId"].AsGuid,
        Email = ReadResults[0]["Email"].AsString,
        UserName = ReadResults[0]["UserName"].AsString
    };

    return Player;
}
```
Source: [PlayersDbAccess](https://github.com/NicholasSchmidt246/NicholasSchmidt246.github.io/blob/master/Sudoku%20C%23%20WebService/Sudoku%20WebService/Sudoku%20WebService/DataAccess/PlayersDbAccess.cs) 

Sample: ReadSeed
``` C#
public async Task<SeedModel> ReadSeed(string difficulty, int dimension, CancellationToken cancellationToken)
{
    var Filter = new BsonDocument
    {
        { "SeedDifficulty", $"{difficulty}" },
        { "SeedDimension", $"{dimension}x{dimension}" }
    };

    var ReadCursorResults = await ReadAsync(Filter, cancellationToken);
    var ReadResults = await ReadCursorResults.ToListAsync(cancellationToken);

    var Rand = new Random();
    int index = Rand.Next(0, ReadResults.Count + 1);

    SeedModel Seed = BsonSerializer.Deserialize<SeedModel>(ReadResults[index].AsBsonDocument);

    return Seed;
}
```
Source: [SeedsDbAccess](https://github.com/NicholasSchmidt246/NicholasSchmidt246.github.io/blob/master/Sudoku%20C%23%20WebService/Sudoku%20WebService/Sudoku%20WebService/DataAccess/SeedsDbAccess.cs) 

### Databases narrative

The artifact was worked as well as planned for demonstrating my skills with Databases. My planned enhancements were to avoid using sessions, reducing the security tests that would be required according to OWASP (Open Web Application Security Project®) ASVS (Application Security Verification Standard) by not using session variables and values. During the code review when it was determined to use seeds to generate game boards, I also gained the need to store them. My initial plan for that, was to use an Azure Storage explorer and treat it just like any other file structure files without them literally sitting on the machine that is running the app service. Do to some headaches that will be covered later, I had to deviate from the plan and include them in the already implemented Azure CosmosDb. I feel like these enhancements demonstrate the following course objective(s): 

1.	Demonstrate an ability to use well-founded and innovative techniques, skills, and tools in computing practices for the purpose of implementing computer solutions that deliver value and accomplish industry-specific goals. 
2.	Develop a security mindset that anticipates adversarial exploits in software architecture and designs to expose potential vulnerabilities, mitigate design flaws, and ensure privacy and enhanced security of data and resources.
 
I have demonstrated (1) by implementing a MongoDb instance of Azure CosmosDb, integrating with a generic base class with specific implementation details in the inheriting classes. This design decision allowed me to submit documents that do not necessarily have to follow a strict structure, SQL would have limited the way the data was stored due to dynamic game board sizes and I would have had to define harder relationships and multiple tables or define some fields as text to get a similar effect. In Mongo, the general object is the same, but the game boards and moves are varying sizes, and that is perfectly okay. The only two collections that I needed initially were my players table and my games table. My Players collection continues to demonstrate (2) by simulating some level of authorization checks after an authentication barrier has been passed. As creating a B2C client on Azure was out of budget I had limited non-hard coded options. After the change of direction away from the storage blob, I added my Seeds collection to CosmosDb further demonstrating my skills using dynamic data storage / access with (1). 
	I had a fairly sizable change of direction for this aspect of the project. I had created and populated the storage blob removing the seeds from the server it is deployed on (Ensuring that future deployments are not necessary only to add seeds, but discovered that in order to read the data, I would need to download them locally to the app service, which is running on a server-less architecture and therefore I do not have clean access to local directory and cleanup for afterwards, nor does the application have permission to write things there while running. I had to change the plan and create the resource in CosmosDb to maintain support of the dynamic data and also allow a random selection of a seed based on difficulty and dimension to support our shuffled seed generation plans. Initially I also had some trouble determining the correct way to serialize the data as I am using Dictionary<int, object> instead of Dictionary<string, object> which would have been supported. I had to do some silly transformation from one to the other in my tests, but in general Serializing directly to a parent object seemed to solve the problem.
