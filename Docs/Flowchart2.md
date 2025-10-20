```mermaid
flowchart TD
  L[Logged-in Menu] --> M[Add Quest]
  L --> N[View All]
  L --> O[Update/Complete]
  L --> P[Advisor Help]
  L --> Q[Show Report]
  M --> R[Set Title/Desc/Due/Priority]
  R --> S{Valid date?}
  S -- No --> M
  S -- Yes --> T[AddQuest()]
  T --> L
  O --> U[Pick Quest by title]
  U --> V{Update or Complete?}
  V -- Update --> W[Change fields] --> T
  V -- Complete --> X[CompleteQuest()] --> L
  P --> Y[GenerateDescription/SuggestPriority/Summarize] --> L