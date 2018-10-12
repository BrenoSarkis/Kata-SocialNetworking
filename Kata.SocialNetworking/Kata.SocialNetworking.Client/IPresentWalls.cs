namespace Kata.SocialNetworking.Client
{
    public interface IPresentWalls
    {
        void PrepareWallFor(string userName);
        UserViewModel ViewModel { get; }
    }
}