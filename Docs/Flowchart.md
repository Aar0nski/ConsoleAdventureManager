```mermaid
flowchart TD
  A[Start] --> B[Main Menu: Register/Login/Exit]
  B -->|Register| C[Enter username/password/contact]
  C --> D{Password strong?}
  D -- No --> C
  D -- Yes --> E[Create User] --> B
  B -->|Login| F[Enter username/password]
  F --> G{Valid?}
  G -- No --> F
  G -- Yes --> H[Send 2FA code (console mock)]
  H --> I[User enters code]
  I --> J{Matches?}
  J -- No --> I
  J -- Yes --> K[Logged-in Menu]
  K -->|Logout| B
  B -->|Exit| Z[End]


