## Git Task

Task Manager backed by Git

It will be a combination of Jira and Microsoft Planner while also having git related capabilities.

The main idea behind the project is the corelation between tasks and folder structure. Since this will be a WPF Desktop Application I can leverage the ability to manipulate folders. The plan is for the tasks to represent a mirror of the folders, starting from the highest one, all the way down to simple task.

The application will be mainly used by me, I use it as a learning oportunity and a showcase of my abilities in WPF. The general structure will follow the MVVM design pattern, a PostgreSQL database and services for API such as Tekla, if needed. First, I need to create a simple MVP, with minimal features and to focus on the core principle and mainly task CRUD operations and folder structure.
