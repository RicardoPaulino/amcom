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

    public static int getTotalScoredGoals(string team, int year)
    {
        HttpClient client = new HttpClient();
        int totalGoals = 0;
        totalGoals += GetGoalsByTeamRole(client, team, year, "team1");
        totalGoals += GetGoalsByTeamRole(client, team, year, "team2");

        return totalGoals;
    }

    private static int GetGoalsByTeamRole(HttpClient client, string team, int year, string role)
    {
        int totalGoals = 0;
        int page = 1;
        bool hasNextPage = true;

        while (hasNextPage)
        {
            string url = $"https://jsonmock.hackerrank.com/api/football_matches?year={year}&{role}={team}&page={page}";
            string json = client.GetStringAsync(url).Result;
            var resultData = JsonConvert.DeserializeObject<ApiResponse>(json)!;
            foreach (var data in resultData.Data!)
            {
                totalGoals += int.Parse(role == "team1" ? data.Team1goals ?? "0" : data.Team2goals ?? "0");
            }
            hasNextPage = resultData.Page < resultData.Total_pages;
            page++;
        }

        return totalGoals;
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