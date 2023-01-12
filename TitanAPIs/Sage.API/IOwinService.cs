namespace TopShelf.Owin.Sage
{
    internal interface IOwinService
    {
        bool Stop();
        bool Start();
    }
}