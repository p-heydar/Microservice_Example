using ElectronBoard.Domain.Entities;

namespace ElectronBoard.Infrastructure.SqlServer.Persistence.SeedData;

public static class StateSeedData
{
    public static List<State> GetAll()
    {
        return new List<State>()
        {
            new State { Name = "Alabama", FipsCode = "01", ElectralVotes = 9 },
            new State { Name = "Alaska", FipsCode = "02", ElectralVotes = 3 },
            new State { Name = "Arizona", FipsCode = "04", ElectralVotes = 11 },
            new State { Name = "Arkansas", FipsCode = "05", ElectralVotes = 6 },
            new State { Name = "California", FipsCode = "06", ElectralVotes = 54 },
            new State { Name = "Colorado", FipsCode = "08", ElectralVotes = 10 },
            new State { Name = "Connecticut", FipsCode = "09", ElectralVotes = 7 },
            new State { Name = "Delaware", FipsCode = "10", ElectralVotes = 3 },
            new State { Name = "Florida", FipsCode = "12", ElectralVotes = 30 },
            new State { Name = "Georgia", FipsCode = "13", ElectralVotes = 16 },
            new State { Name = "Hawaii", FipsCode = "15", ElectralVotes = 4 },
            new State { Name = "Idaho", FipsCode = "16", ElectralVotes = 4 },
            new State { Name = "Illinois", FipsCode = "17", ElectralVotes = 19 },
            new State { Name = "Indiana", FipsCode = "18", ElectralVotes = 11 },
            new State { Name = "Iowa", FipsCode = "19", ElectralVotes = 6 },
            new State { Name = "Kansas", FipsCode = "20", ElectralVotes = 6 },
            new State { Name = "Kentucky", FipsCode = "21", ElectralVotes = 8 },
            new State { Name = "Louisiana", FipsCode = "22", ElectralVotes = 8 },
            new State { Name = "Maine", FipsCode = "23", ElectralVotes = 4 },
            new State { Name = "Maryland", FipsCode = "24", ElectralVotes = 10 },
            new State { Name = "Massachusetts", FipsCode = "25", ElectralVotes = 11 },
            new State { Name = "Michigan", FipsCode = "26", ElectralVotes = 15 },
            new State { Name = "Minnesota", FipsCode = "27", ElectralVotes = 10 },
            new State { Name = "Mississippi", FipsCode = "28", ElectralVotes = 6 },
            new State { Name = "Missouri", FipsCode = "29", ElectralVotes = 10 },
            new State { Name = "Montana", FipsCode = "30", ElectralVotes = 4 },
            new State { Name = "Nebraska", FipsCode = "31", ElectralVotes = 5 },
            new State { Name = "Nevada", FipsCode = "32", ElectralVotes = 6 },
            new State { Name = "New Hampshire", FipsCode = "33", ElectralVotes = 4 },
            new State { Name = "New Jersey", FipsCode = "34", ElectralVotes = 14 },
            new State { Name = "New Mexico", FipsCode = "35", ElectralVotes = 5 },
            new State { Name = "New York", FipsCode = "36", ElectralVotes = 28 },
            new State { Name = "North Carolina", FipsCode = "37", ElectralVotes = 16 },
            new State { Name = "North Dakota", FipsCode = "38", ElectralVotes = 3 },
            new State { Name = "Ohio", FipsCode = "39", ElectralVotes = 17 },
            new State { Name = "Oklahoma", FipsCode = "40", ElectralVotes = 7 },
            new State { Name = "Oregon", FipsCode = "41", ElectralVotes = 8 },
            new State { Name = "Pennsylvania", FipsCode = "42", ElectralVotes = 19 },
            new State { Name = "Rhode Island", FipsCode = "44", ElectralVotes = 4 },
            new State { Name = "South Carolina", FipsCode = "45", ElectralVotes = 9 },
            new State { Name = "South Dakota", FipsCode = "46", ElectralVotes = 3 },
            new State { Name = "Tennessee", FipsCode = "47", ElectralVotes = 11 },
            new State { Name = "Texas", FipsCode = "48", ElectralVotes = 40 },
            new State { Name = "Utah", FipsCode = "49", ElectralVotes = 6 },
            new State { Name = "Vermont", FipsCode = "50", ElectralVotes = 3 },
            new State { Name = "Virginia", FipsCode = "51", ElectralVotes = 13 },
            new State { Name = "Washington", FipsCode = "53", ElectralVotes = 12 },
            new State { Name = "West Virginia", FipsCode = "54", ElectralVotes = 4 },
            new State { Name = "Wisconsin", FipsCode = "55", ElectralVotes = 10 },
            new State { Name = "Wyoming", FipsCode = "56", ElectralVotes = 3 }
        };
    }
}