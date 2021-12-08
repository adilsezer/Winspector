using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Automation;

namespace UIAElementModel
{
    public class UIAElement
    {
        public string Name { get; private set; }
        public string ClassName { get; private set; }
        public string AutomationId { get; private set; }
        public string ProcessId { get; private set; }
        public string ControlType { get; private set; }
        public string ParentName { get; private set; }
        public string ParentClassName { get; private set; }
        public string ParentControlType { get; private set; }
        public string ParentAutomationId { get; private set; }

        public UIAElement(AutomationElement element)
        {
            if (element == null) return;

            try
            {
                Name = element.Current.Name;
                ClassName = element.Current.ClassName;
                AutomationId = element.Current.AutomationId;
                ProcessId = element.Current.ProcessId.ToString();
                ControlType = element.Current.ControlType.ToString();
                AutomationElement parentElement = TreeWalker.ControlViewWalker.GetParent(element);
                if (parentElement != null)
                {
                    ParentName = parentElement.Current.Name;
                    ParentClassName = parentElement.Current.ClassName;
                    ParentControlType = parentElement.Current.ControlType.ToString();
                    ParentAutomationId = parentElement.Current.AutomationId;
                }
            }
            catch (Exception)
            {
                return;
            }
        }

        public static AutomationElement GetAutomationElementFromPoint(int x, int y)
        {
            try
            {
                return AutomationElement.FromPoint(new System.Windows.Point(x, y));
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}