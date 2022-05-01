namespace backend_dev_project.Services;

public interface IUserNotificationService {
    void UserAdded(UserInfo user);
}
public class UserNotificationService : IUserNotificationService {

    public void UserAdded(UserInfo user) {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("RecepeAPI", "jonas.faber@student.howest.be"));
            message.To.Add(new MailboxAddress("Jonahas", "jonas.h.faber@gmail.com"));
            message.Subject = "User has been added!";

            message.Body = new TextPart("plain"){
                Text = $@"Hey Jonas,
                    A user has been added: {user.username}.
                    Howdoe!
                "
            };

            Console.WriteLine($"Mail message has been send! User has been added!{user.username}");
    }
    
}