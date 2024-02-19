using FileParser.Dto;

namespace FileParser.Services;

public static class ChangeRandomModuleState
{
    private static string[] statuses = { "Online", "Run", "NotReady", "Offline" };

    public static void ChangeStatus(ModuleStateDto[] modules)
    {
        foreach (var module in modules)
        {
            module.ModuleState = GetRandomStatus(module.ModuleState);
        }
    }
    
    private static string GetRandomStatus(string state)
    {
        var random = new Random();
        var index = random.Next(statuses.Length - 1);
        return statuses.Where(c => c != state).ToArray()[index];
    }
}