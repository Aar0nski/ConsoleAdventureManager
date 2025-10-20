```mermaid
classDiagram
  class User {
    +string Username
    +string PasswordHash
    +string EmailOrPhone
  }

  class Quest {
    +string Title
    +string Description
    +DateTime DueDate
    +Priority Priority
    +bool IsCompleted
  }

  class QuestManager {
    -List~Quest~ quests
    +AddQuest(Quest)
    +GetAll(): List~Quest~
    +UpdateQuest(title, updates)
    +CompleteQuest(title)
    +GetDueSoon(hours): List~Quest~
  }

  class Authenticator {
    +Register(username, password, contact): bool
    +Login(username, password): bool
    +Send2FACode(user): void
    +Verify2FA(user, code): bool
  }

  class NotificationService {
    +CheckAndNotify(quests): void
  }

  class GuildAdvisorAI {
    +GenerateDescription(title): string
    +SuggestPriority(quest): Priority
    +Summarize(quests): string
  }

  class MenuHelper {
    +ShowMain()
    +ShowLoggedIn()
  }

  User "1" --> "many" Quest : owns
  QuestManager --> Quest
  NotificationService --> Quest
  GuildAdvisorAI --> Quest