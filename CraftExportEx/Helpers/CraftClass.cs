using CraftExportEx;

namespace CraftExport.Helpers
{
    public class CraftClass
    {
        /// <summary>
        /// The name of the craft class.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// The type of craft class.
        /// </summary>
        public ClassJobType Type { get; set; }
        /// <summary>
        /// A list of available actions for the current class.
        /// </summary>
        public ActionList Actions { get; set; }

        /// <summary>
        /// Loads the requested class data.
        /// </summary>
        /// <param name="classType"></param>
        ///             

        public void Load(ClassJobType classType)
        {
            // Set the name.
            Name = classType.ToString();
            // Set the ttype.
            Type = classType;

            // Switch between the type and load the proper list.
            switch (classType)
            {
                case ClassJobType.Carpenter:
                    Name = "刻木匠";
                    Actions = new CarpenterActions();
                    break;
                case ClassJobType.Blacksmith:
                    Name = "锻铁匠";
                    Actions = new BlacksmithActions();
                    break;
                case ClassJobType.Armorer:
                    Name = "铸甲匠";
                    Actions = new ArmorerActions();
                    break;
                case ClassJobType.Goldsmith:
                    Name = "雕金匠";
                    Actions = new GoldsmithActions();
                    break;
                case ClassJobType.Leatherworker:
                    Name = "制革匠";
                    Actions = new LeatherworkerActions();
                    break;
                case ClassJobType.Weaver:
                    Name = "裁衣匠";
                    Actions = new WeaverActions();
                    break;
                case ClassJobType.Alchemist:
                    Name = "炼金术士";
                    Actions = new AlchemistActions();
                    break;
                case ClassJobType.Culinarian:
                    Name = "烹调师";
                    Actions = new CulinarianActions();
                    break;
            }
        }
    }
}