using SimApi.Base;

namespace SimApi.Operation;

public interface INotificationService
{
    ApiResponse ProduceEmail(MailInfo request);
    ApiResponse ConsumeEmail();
}
