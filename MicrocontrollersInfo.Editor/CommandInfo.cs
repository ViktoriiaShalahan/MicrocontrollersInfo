namespace MicrocontrollersInfo.Editor
{

    delegate void Command();

    struct CommandInfo {
        public string name;
        public Command command;

        public CommandInfo(string name, Command command) {
            this.name = name;
            this.command = command;
        }
    }
}