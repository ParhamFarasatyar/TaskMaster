# TaskMaster

TaskMaster is a .NET console application designed for a lightweight task-evaluation workflow. It models a small training platform where users are assigned roles, questions are created and managed, and answers are submitted, reviewed, and scored.

The project is built around a simple console menu system and uses local JSON files as its database layer. It is not a web application or API; it is a self-contained desktop-like application for learning and internal task management.

## What the project does

TaskMaster supports two main user roles:

- Designer
  - Registers and logs in
  - Creates coding/design tasks with a description, difficulty, and score
  - Edits or removes existing tasks
  - Reviews submitted answers from interns
  - Approves or rejects answers
  - Awards score to interns and updates their level

- Intern
  - Registers and logs in
  - Views their profile and account details
  - Answers tasks available for their current level
  - Checks the status of their submitted answers
  - Gains score when a designer approves an answer

The project implements a complete loop of:

1. User registration and login
2. Role-based navigation
3. Task creation and management
4. Answer submission
5. Review and approval workflow
6. Score and level progression

## Core logic and behavior

### 1. User system
The app manages users through the `User` model in `TaskMaster/DataModel/User.cs`.

User properties include:

- `Role` (`Designer` or `Intern`)
- `Name`
- `LastName`
- `UserName`
- `Password`
- `Level` (`Beginner`, `MidLevel`, `Advanced`)
- `Score`

The user model includes these important behaviors:

- `Add()` creates a new user only if the username is unique
- `Edit()` updates profile information, with username uniqueness validation
- `Remove()` deletes the user and removes their answers from storage
- `SetScore()` adds points to a user's score
- `UpdateLevel()` recalculates their progression tier based on score

Score rules implemented in the code:

- `Score <= 10` → `Beginner`
- `10 < Score <= 20` → `MidLevel`
- `Score > 20` → `Advanced`

### 2. Question system
Questions are represented by the `Question` class in `TaskMaster/DataModel/Question.cs`.

Each question contains:

- `Description`
- `Grade`
- `Difficulty` (`Beginner`, `MidLevel`, `Advanced`)
- `Id`
- `CreatedAt`
- `UpdatedAt`

Designer actions:

- Create a question by entering: description, grade (1–5), and difficulty
- Edit question fields (description, difficulty, grade)
- Remove a question and delete any related answers for that task

The app also filters available questions for each intern using:

- question difficulty matching the intern's current level
- excluding already answered questions for that user

This logic is handled by `Question.GetQuestionsUserCanAnswer(...)`.

### 3. Answer system
Answers are represented by the `Answer` class in `TaskMaster/DataModel/Answer.cs`.

Each answer includes:

- `UserName`
- `AnswerId`
- `ApprovalStatus` (`Pending`, `Approve`, `Reject`)
- `CreatedDate`
- `QuestionId`
- `Code`
- `Grade`
- `GoalGrade`
- `Description`

Interns submit code answers tied to a selected task. Designers later review pending answers and choose to:

- Approve: sets status to `Approve`, assigns the goal grade, adds the grade to the user's score, and updates the level
- Reject: sets status to `Reject` and assigns grade `0`

### 4. Menu-driven application flow
The UI is fully console-based. The app uses menu classes under `TaskMaster/Menu/` to guide the user through actions.

Main flow:

- `Program.cs` starts the app in a loop
- `MainMenu` presents `Login`, `Register`, or `Exit`
- Based on the authenticated user role:
  - `InternMenu` opens intern features
  - `DesignerMenu` opens designer features

These menus are implemented with `ConsoleMenu.Show(...)`, which supports keyboard navigation and selection with arrow keys and Enter.

### 5. Validation and UX rules
Validation rules are centralized in `TaskMaster/System/SystemValidation.cs`.

The app validates:

- non-empty strings
- trimmed input
- username length and character rules
- password strength requirements
- grade range (`1` to `5`)

The CLI also includes helper methods for message colors, countdown transitions, and input handling, giving the app a polished terminal experience.

## Data storage
The application persists state in JSON files instead of a relational database.

The storage layer is in `TaskMaster/Database/Database.cs`.

It stores data under the runtime output directory, in a `Data` folder, for example:

- `TaskMaster/bin/Debug/net10.0/Data/Users.json`
- `TaskMaster/bin/Debug/net10.0/Data/Questions.json`
- `TaskMaster/bin/Debug/net10.0/Data/Answers.json`

The JSON files are managed by `Database.Save(...)`, `Database.Load(...)`, and `Database.Update(...)`.

This means the app is effectively a local, file-based data system rather than a hosted multi-user platform.

## Project structure

```text
TaskMaster/
├── DataModel/
│   ├── Answer.cs
│   ├── Question.cs
│   └── User.cs
├── Database/
│   └── Database.cs
├── Menu/
│   ├── AnswerQuestion.cs
│   ├── AnswerStatus.cs
│   ├── ConsoleHelper.cs
│   ├── ConsoleMenu.cs
│   ├── DesignerMenu.cs
│   ├── EditQuestion.cs
│   ├── InternMenu.cs
│   ├── Login.cs
│   ├── MainMenu.cs
│   ├── MenuManager.cs
│   ├── Profile.cs
│   ├── QuestionCreator.cs
│   ├── Register.cs
│   ├── RemoveQuestion.cs
│   └── ReviewAnswer.cs
├── System/
│   └── SystemValidation.cs
├── Program.cs
├── TaskMaster.csproj
└── obj/ and bin/ (generated build artifacts)
```

## How the application works in practice

A typical flow looks like this:

1. The user launches the app.
2. They either log in or create a new account.
3. If they are a designer:
   - they create tasks
   - interns submit answers
   - the designer approves or rejects answers
   - user scores are updated
4. If they are an intern:
   - they answer tasks matching their level
   - they check answer status
   - their score changes only after review approval

This creates a simple evaluation cycle where the designer acts as an instructor and the intern acts as a learner.

## Getting started

### Requirements

- .NET SDK compatible with `net10.0`
- A terminal or command prompt

### Run the project

From the repository root:

```bash
dotnet build "TaskMaster\TaskMaster.csproj"
dotnet run --project "TaskMaster\TaskMaster.csproj"
```

Or, from inside the project directory:

```bash
cd TaskMaster
dotnet run
```

## Notes and limitations

This project is a strong demonstration of a console-based task management flow, but it is intentionally simple and has some limitations:

- Data is stored locally in JSON files, not in a database server
- Passwords are stored in plain JSON and are not hashed or encrypted
- There is no web API, authentication server, or multi-user backend
- The application is designed for in-terminal usage and local learning scenarios
- There are no automated tests in the project at the moment

## Summary

TaskMaster is essentially a mini internal training system for creating and reviewing tasks. It combines role-based menus, user management, task handling, answer tracking, scoring, and level progression in a single console application.

It is a practical example of how a small business workflow can be modeled using C#, JSON storage, and a menu-driven interface.
