namespace backend_dev_project.Services;

public interface IChatService {
    bool ContainsBadWords(Question q, List<string> BadWords);
}

public class ChatService : IChatService {

    public bool ContainsBadWords(Question q, List<string> BadWords) {

        var containsBadWordWords =BadWords.Any(substring => q.question.Contains(substring, StringComparison.CurrentCultureIgnoreCase));

        return containsBadWordWords;
    }
}