using Newtonsoft.Json;

public class Program
{
    public static void Main()
    {
        string teamName = "Paris Saint-Germain";
        int year = 2013;
        int totalGoals = getTotalScoredGoals(teamName, year);

        Console.WriteLine("Team " + teamName + " scored " + totalGoals.ToString() + " goals in " + year);

        teamName = "Chelsea";
        year = 2014;
        totalGoals = getTotalScoredGoals(teamName, year);

        Console.WriteLine("Team " + teamName + " scored " + totalGoals.ToString() + " goals in " + year);

        // Output expected:
        // Team Paris Saint - Germain scored 109 goals in 2013
        // Team Chelsea scored 92 goals in 2014
    }

    public static int getTotalScoredGoals(string teamName, int year)
    {
        int totalGoals = 0;
        HttpClient client = new HttpClient();
        foreach(string teamRole in new string[] { "team1", "team2" })
        {            
            int page = 1;
            bool hasNextPage = true;
            
            while (hasNextPage)
            {
                var responseTeam = GetGoalsByTeamRole(client, teamName, year, teamRole, page);

                foreach (var data in responseTeam.Data!)
                {
                    totalGoals += int.Parse(teamRole == "team1" ? data.Team1goals!: data.Team2goals!);
                }

                hasNextPage = page < responseTeam.Total_pages;
                page++;
            }            
        }
        return totalGoals;
    }

    private static ApiResponse GetGoalsByTeamRole(HttpClient client, string team, int year, string role, int page)
    {
        string url = $"https://jsonmock.hackerrank.com/api/football_matches?year={year}&{role}={team}&page={page}";
        string json = client.GetStringAsync(url).Result;
        var resultData = JsonConvert.DeserializeObject<ApiResponse>(json)!;

        return resultData;
    }
}

public class ApiResponse
{
    public int Page { get; set; }
    public int Total_pages { get; set; }
    public List<ChampionShipResult>? Data { get; set; }
}

public class ChampionShipResult
{
    public string? Team1goals { get; set; }
    public string? Team2goals { get; set; }
}