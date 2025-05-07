namespace Game.Services.Network;

public class Authorization
{
    public bool IsAuthorized { get; set; }
    public string Status { get; set; } = "loading";
    
    
    public void Entrance()
    {
        // TODO: STEAM AUTH HERE
        
        Auth();
    }
    
    protected async void Auth()
    {
        try
        {
            Console.WriteLine($"Старт авторизации");
            
            await AuthRequest();
            IsAuthorized = true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка Авторизации: {ex}");
            
            IsAuthorized = false;
        }
        finally
        {
            Console.WriteLine($"Авторизация закончена");
        }
    }
    
    public async Task AuthRequest()
    {
        Status = "Connecting...";
        await Task.Delay(2000); 
        
        Status = "Steam Auth...";
        await Task.Delay(2000); 
        
        Status = "Authorized";
    }
}