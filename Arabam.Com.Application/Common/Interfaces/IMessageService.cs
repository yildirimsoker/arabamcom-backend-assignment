namespace Arabam.Com.Application.Common.Interfaces
{
    public interface IMessageService
    {
        bool Enqueue(string message);
    }
}
