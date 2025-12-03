using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace COMP1551
{
    public static class InputHelper
    {
        public static string GetStringInput(string prompt)
        {
            string input;
            do
            {
                Console.Write(prompt);
                input = Console.ReadLine();
                
                // Use the logical NOT operator (!) with string.IsNullOrWhiteSpace
                // This checks if the string contains actual, visible characters.
                if (!string.IsNullOrWhiteSpace(input))
                {
                    return input;
                }
                Console.WriteLine("This field cannot be empty !!!");
                
            } while (true);
        }
        // Reusable method for getting a valid integer
        public static int GetIntInput(string prompt)
        {
            int result;
            string input;
            do
            {
                Console.Write(prompt);
                input = Console.ReadLine();
                if (int.TryParse(input, out result) && result >= 0)
                {
                    return result;
                }
                Console.WriteLine("Invalid input. Please enter a whole number.");
            } while (true);
        }

        // Reusable method for getting a valid double (for salary)
        public static double GetDoubleInput(string prompt)
        {
            double result;
            string input;
            do
            {
                Console.Write(prompt);
                input = Console.ReadLine();
                // Using InvariantCulture to handle standard decimal formats 
                // System.Globalization.NumberStyles.Any allows scientific notation, decimal...
                if (double.TryParse(input, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out result) && result >= 0)
                {
                    return result;
                }
                Console.WriteLine("Invalid input. Please enter a valid number (e.g., 50000 or 50000.50).");
            } while (true);
        }
        public static string ValidateEmail()
        {
            string input_email=GetStringInput("Enter Email: ");
            while (
                !input_email.EndsWith("@gmail.com") &&
                !input_email.EndsWith("@outlook.com")
            )//only ends loop when 1 of them right , false and right is false-->end
            {
                input_email = GetStringInput("Email must contain @gmail.com\nEnter email again: ");
            }
            return input_email;
        }
    }


    public class Person
    {
        //default constructor to say welcome
        public Person() { Console.WriteLine("Welcome"); }
        private string name;
        public void setName()
        {
            name=InputHelper.GetStringInput("Enter your name: ");
        }
        public string getName() { return name; }

        private int telephone;
        public void setTelephone()
        {
            while(true)
            {
            int inputTelephone =InputHelper.GetIntInput("Enter phone number: ");
            string phoneCheck=inputTelephone.ToString();
            if(phoneCheck.Length>6 && phoneCheck.Length < 11)
                {
                    telephone=inputTelephone;
                    break;
                }
            Console.WriteLine("phone number is around 7-10 digits");
            }
 
        }
        public int getTelephone() { return telephone; }

        private string email;
        public void setEmail()
        {
            while(true){
                email = InputHelper.ValidateEmail();
                List<string> users = UserManager.loadAllUsers("/Users/dangquangvinh/Desktop/COMP1551/database.csv");
                bool emailExist=false;
                foreach ( string user in users)
                {
                    string[] line= user.Split(',');
                    if (email == line[2])
                    {
                        emailExist=true;
                        Console.WriteLine("this is an existing email !!!");
                        break;
                    }
                }
                if (!emailExist)
                {
                    break;
                }
                
            
            }
            



        }
        public string getEmail() { return email; }

        private string role;
        public void setRole()
        {
            Console.WriteLine("1. Student\n2. Teacher\n3. Admin");//show list of role to minimize mistakes
            int choose = InputHelper.GetIntInput("Choose a number:");
            switch (choose)
            {
                case 1: role = "Student"; break;
                case 2: role = "Teacher"; break;
                case 3: role = "Admin"; break;
                default: role = "Unknown"; Console.WriteLine("Invalid role selected."); break;
            }
        }
        public string getRole() { return role; }

        //override method from defaut baseclass object ToString automatically called with Writeline
        public override string ToString()
        {
            return $"{name},{telephone},{email},{role}\n";
        }
    }

    public class Student : Person
    {
        private string[] subject = new string[3];//store 3 subjects of student
        public void setSubject()
        {
            string subject_file = "/Users/dangquangvinh/Desktop/COMP1551/subject.csv";
            List<string> subjects = File.ReadAllLines(subject_file).ToList();

            // Create a list to store subject names (ignore header)
            List<string> subjectNames = new List<string>();

            for (int i = 1; i < subjects.Count; i++)
            {
                string[] fields = subjects[i].Split(',');
                string subjectName = fields[1].Trim();
                subjectNames.Add(subjectName);

                Console.WriteLine($"{i}. {subjectName}");
            }

            // Ask user to choose subject 1
            int choice1 = InputHelper.GetIntInput("Choose Subject 1 (number): ");
            subject[0] = subjectNames[choice1 - 1];

            // Ask user to choose subject 2 (must be different)
            int choice2;
            while (true)
            {
                choice2 = InputHelper.GetIntInput("Choose Subject 2 (number): ");
                if (choice2 != choice1)
                {
                    subject[1] = subjectNames[choice2 - 1];
                    break;
                }
                Console.WriteLine("Subject 2 must be different from Subject 1. Try again.");
            }

            // Ask user to choose subject 3 (must be different from both)
            int choice3;
            while (true)
            {
                choice3 = InputHelper.GetIntInput("Choose Subject 3 (number): ");
                if (choice3 != choice1 && choice3 != choice2)
                {
                    subject[2] = subjectNames[choice3 - 1];
                    break;
                }
                Console.WriteLine("Subject 3 must be different from previous choices. Try again.");
            }
        }

        public string getSubject()
        {
            return $"{subject[0]},{subject[1]},{subject[2]}";
        }
    }

    public class Teacher : Person
    {
        private string[] subject = new string[3];
        public void setSubject()
        {
            string subject_file = "/Users/dangquangvinh/Desktop/COMP1551/subject.csv";
            List<string> subjects = File.ReadAllLines(subject_file).ToList();

            // Create a list to store subject names (ignore header)
            List<string> subjectNames = new List<string>();

            for (int i = 1; i < subjects.Count; i++)
            {
                string[] fields = subjects[i].Split(',');
                string subjectName = fields[1].Trim();
                subjectNames.Add(subjectName);

                Console.WriteLine($"{i}. {subjectName}");
            }

            int choice1 = InputHelper.GetIntInput("Subject 1: ");
            subject[0]=subjectNames[choice1-1];
            int choice2;
            while (true)
            {
                choice2=InputHelper.GetIntInput("Subject 2: ");
                if (choice2 != choice1)
                {
                    subject[1]=subjectNames[choice2-1];
                    break;
                }
                Console.WriteLine("Subject 2 must be different from Subject 1. Try again.");
            }
            subject[2] = $"XXX"; // Only 2 subjects are asked
            //XXX means the class does not have this column
        }
        public string getSubject()
        {
            return $"{subject[0]},{subject[1]}";
        }
        private string salary;
        public void setSalary()
        {
            // using validation helper
            double salaryValue = InputHelper.GetDoubleInput("Enter salary: ");
            salary = salaryValue.ToString();
        }
        public string getSalary(){return salary;}
    }

    public class Admin : Person
    {
        private string salary;
        private string status;
        public void setSalary()
        {
            // using validation helper
            double salaryValue = InputHelper.GetDoubleInput("Enter salary: ");//get double input from user
            salary = salaryValue.ToString();
        }
        public string getSalary(){return salary;}
        public void setSatus()
        {
            Console.WriteLine("Enter status:\n1. Fulltime\n2. Partime");//Admin staff can only choose Fulltime or Partime
            
            while (true)
            {
                int choose = InputHelper.GetIntInput("Choose a number: ");
                if (choose == 1)
                {
                    status="Fulltime";break;
                }
                else if (choose == 2)
                {
                    status="Partime";break;
                }
            }
        }
        public string getStatus(){return status;}
        private int workingHours;
        public void setWorkingHours()
        {
            workingHours = InputHelper.GetIntInput("Enter working hours in number: ");
        }
        public int getWorkingHours()
        {
            return workingHours;
        }
    }

    public class UserManager
    {
        public static List<string> loadAllUsers(string path)//class is used to load user
        {
            try
            {
                if (File.Exists(path))
                {
                    return File.ReadAllLines(path).ToList();
                }
                else
                {
                    // Create file with header if it doesn't exist
                    string header = "Name,Telephone,Email,Role,Subject1,Subject2,Subject3,Salary,Status,WorkingHours";
                    File.WriteAllText(path, header + Environment.NewLine);
                    return new List<string> { header };
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading file: {ex.Message}");
                return new List<string> { "Name,Telephone,Email,Role,Subject1,Subject2,Subject3,Salary,Status,WorkingHours" };
            }
        }

        // class is used to save data
        private void saveAllUsers(string path, List<string> lines)
        {
            File.WriteAllLines(path, lines);
        }

        public void addUser(string path, string user)
        {
            File.AppendAllText(path, user);
        }

        public string chooseFunction()//show the menu of function and ask user to choose
        {
            Console.WriteLine("\n====================================");
            Console.WriteLine("1. Add User");
            Console.WriteLine("2. Edit User");
            Console.WriteLine("3. Delete User");
            Console.WriteLine("4. List All Users");
            Console.WriteLine("5. List User by Role");
            Console.WriteLine("6. Add Subject");
            Console.WriteLine("7. List Students by Subject");
            Console.WriteLine("8. List Teachers by Subject");
            Console.WriteLine("0. Exit");
            Console.WriteLine("====================================");
            Console.Write("Choose a number: ");
            return Console.ReadLine();
        }

        public void handleAddUser(string path)
        {
            Console.Clear();
            Person user = new Person();
            user.setName();
            user.setTelephone();
            user.setEmail();
            user.setRole();

            string role = user.getRole();
            if (role == "Unknown") return;

            string info = "";//used to add to database
            string info_confirm="";//used to confirm with user

            if (role == "Student")
            {
                Student student = new Student();
                student.setSubject();
                // Format: baseInfo + subject[0], subject[1], subject[2], XXX, XXX, XXX
                info = $"{user.getName()},{user.getTelephone()},{user.getEmail()},{role},{student.getSubject()},XXX,XXX,XXX\n";
                string[] subjects=student.getSubject().Split(',');
                info_confirm=$"Name: {user.getName()}\nPhone: {user.getTelephone()}\nEmail: {user.getEmail()}\nRole: {role}\nSubject 1: {subjects[0]}\nSubject 2: {subjects[1]}\nSubject 3: {subjects[2]}";
            }
            else if (role == "Teacher")
            {
                Teacher teacher = new Teacher();
                teacher.setSubject();
                teacher.setSalary();
                // Format: baseInfo + subject[0], subject[1], XXX, salary, XXX, XXX
                info = $"{user.getName()},{user.getTelephone()},{user.getEmail()},{role},{teacher.getSubject()},XXX,{teacher.getSalary()},XXX,XXX\n";
                string[] subjects=teacher.getSubject().Split(',');
                info_confirm=$"Name: {user.getName()}\nPhone: {user.getTelephone()}\nEmail: {user.getEmail()}\nRole: {role}\nSubject 1: {subjects[0]}\nSubject 2: {subjects[1]}\nSalary: {teacher.getSalary()}";
            }
            else if (role == "Admin")
            {
                Admin admin = new Admin();
                admin.setSatus();
                admin.setWorkingHours();
                admin.setSalary();
                // Format: baseInfo + XXX, XXX, XXX, salary, status, workingHours
                info = $"{user.getName()},{user.getTelephone()},{user.getEmail()},{role},XXX,XXX,XXX,{admin.getSalary()},{admin.getStatus()},{admin.getWorkingHours()}\n";
                info_confirm=$"Name: {user.getName()}\nPhone: {user.getTelephone()}\nEmail: {user.getEmail()}\nRole: {role}\nStatus: {admin.getStatus}\nSalary: {admin.getSalary()}\nWorking hours: {admin.getWorkingHours()}";
            }

            Console.Clear();
            Console.WriteLine($"--- Data to Add ---");
            Console.WriteLine(info_confirm);
            Console.WriteLine("Hit Enter to add this user.");
            
            if (Console.ReadKey().Key == ConsoleKey.Enter) { addUser(path, info); Console.WriteLine("\nUser added successfully!"); }
        }

        public void handleEditUser(string path)
        {
            Console.Clear();
            List<string> lines = loadAllUsers(path);

            if (lines.Count <= 1) { Console.WriteLine("No users to edit."); return; }

            string header = lines[0];
            Console.WriteLine("===== CURRENT USER LIST (Choose a user to edit) =====");
            Console.WriteLine(header);

            for (int i = 1; i < lines.Count; i++) { Console.WriteLine($"{i}. {lines[i]}"); }

            int indexToEdit = InputHelper.GetIntInput("\nEnter the number of the row you want to edit: ");

            if (indexToEdit < 1 || indexToEdit >= lines.Count)
            {
                Console.WriteLine("Invalid selection.");
                return;
            }

            string existingLine = lines[indexToEdit];
            string[] fields = existingLine.Split(',');
            string role = fields[3];

            Console.WriteLine($"\nYou are editing: {existingLine}");
            Console.WriteLine($"\n--- Editing {role} Data ---");

            Person editedUser;

            //Create a new object based on the existing role and get new data
            switch (role)
            {
                case "Student": editedUser = new Student(); ((Student)editedUser).setSubject(); break;
                case "Teacher": editedUser = new Teacher(); ((Teacher)editedUser).setSubject(); ((Teacher)editedUser).setSalary(); break;
                case "Admin": editedUser = new Admin(); ((Admin)editedUser).setSatus(); ((Admin)editedUser).setWorkingHours(); ((Admin)editedUser).setSalary(); break;
                default: Console.WriteLine("Error: Unknown role found in data. Cannot edit."); return;
            }

            // Get and confirm core Person data (assuming full re-entry for simplicity)
            editedUser.setName();
            editedUser.setTelephone();
            editedUser.setEmail();

            // Create the new updated line for the CSV
            string newLine = "";
            string baseInfo = $"{editedUser.getName()},{editedUser.getTelephone()},{editedUser.getEmail()},{role}";

            if (role == "Student")
            {
                Student student = (Student)editedUser;
                newLine = $"{baseInfo},{student.getSubject()},XXX,XXX,XXX";
            }
            else if (role == "Teacher")
            {
                Teacher teacher = (Teacher)editedUser;
                newLine = $"{baseInfo},{teacher.getSubject()},XXX,{teacher.getSalary()},XXX,XXX";
            }
            else if (role == "Admin")
            {
                Admin admin = (Admin)editedUser;
                newLine = $"{baseInfo},XXX,XXX,XXX,{admin.getSalary()},{admin.getStatus()},{admin.getWorkingHours()}";
            }

            // Update the List and Write back to file
            lines[indexToEdit] = newLine;

            Console.Clear();
            Console.WriteLine("===== UPDATED DATA CONFIRMATION =====");
            Console.WriteLine($"Old data was: {existingLine}");
            Console.WriteLine($"New data is:  {newLine}");

            Console.Write("\nPress Enter to save changes to the file: ");
            if (Console.ReadKey().Key == ConsoleKey.Enter)
            {
                saveAllUsers(path, lines);
                Console.WriteLine("\nUser updated successfully!");
            }
            else
            {
                Console.WriteLine("\nUpdate cancelled.");
            }
        }

        public void handleDeleteUser(string path)
        {
            Console.Clear();
            List<string> lines = loadAllUsers(path);

            if (lines.Count <= 1) { Console.WriteLine("No users to delete."); return; }

            Console.WriteLine("===== CURRENT USER LIST =====");
            Console.WriteLine(lines[0]);

            for (int i = 1; i < lines.Count; i++) { Console.WriteLine($"{i}. {lines[i]}"); }

            int indexToDelete = InputHelper.GetIntInput("\nEnter the number of the row you want to delete: ");

            if (indexToDelete < 1 || indexToDelete >= lines.Count)
            {
                Console.WriteLine("Invalid selection.");
                return;
            }

            Console.WriteLine($"\nAre you sure you want to delete:");
            Console.WriteLine(lines[indexToDelete]);
            Console.Write("Press Enter to confirm: ");

            if (Console.ReadKey().Key == ConsoleKey.Enter)
            {
                lines.RemoveAt(indexToDelete);
                saveAllUsers(path, lines);
                Console.WriteLine("\nRow deleted successfully!");
            }
            else
            {
                Console.WriteLine("\nDelete cancelled.");
            }
        }

        public void handleListAllUsers(string path)
        {
            Console.Clear();
            List<string> lines = loadAllUsers(path);

            if (lines.Count <= 1) { Console.WriteLine("No users to list."); return; }

            Console.WriteLine("===== CURRENT USER LIST =====");
            Console.WriteLine(lines[0]);

            for (int i = 1; i < lines.Count; i++)
            {
                Console.WriteLine($"{i}. {lines[i]}");
            }
        }

        public void handleListByRole(string path)
        {
            Console.Clear();
            List<string> lines = loadAllUsers(path);

            // If only the header exists, there are no users
            if (lines.Count <= 1)
            {
                Console.WriteLine("No users available to list by role.");
                return;
            }

            List<string[]> userData = new List<string[]>();

            for (int i = 1; i < lines.Count; i++)  // start at 1 to skip header
            {
                string line = lines[i];
                string[] fields = line.Split(',');

                if (fields.Length >= 4)
                {
                    userData.Add(fields);
                }
            }

            //Group data by role (fields[3] = Role)
            Dictionary<string, List<string[]>> groupedByRole = new Dictionary<string, List<string[]>>();

            foreach (var user in userData)
            {
                string role = user[3];

                if (!groupedByRole.ContainsKey(role))
                {
                    groupedByRole[role] = new List<string[]>();
                }

                groupedByRole[role].Add(user);
            }

            Console.WriteLine("===== USER LIST BY ROLE =====");

            foreach (var roleGroup in groupedByRole)
            {
                string roleName = roleGroup.Key;
                List<string[]> usersInThisRole = roleGroup.Value;

                Console.WriteLine($"\n--- Role: {roleName} ({usersInThisRole.Count} users) ---");

                // Print the header row
                Console.WriteLine(lines[0]);

                // Print all users under this role
                foreach (var userFields in usersInThisRole)
                {
                    Console.WriteLine(string.Join(",", userFields));
                }
            }
        }
        public void handleAddSubject()
        {
            string subject_path = "/Users/dangquangvinh/Desktop/COMP1551/subject.csv";

            // Load all lines
            List<string> lines = File.ReadAllLines(subject_path).ToList();

            // Extract subjects (skip header)
            HashSet<string> existingSubjects = new HashSet<string>();
            for (int i = 1; i < lines.Count; i++)
            {
                string[] fields = lines[i].Split(',');
                if (fields.Length >= 2)
                {
                    existingSubjects.Add(fields[1].Trim().ToLower());
                }
            }

            // Ask user for a subject repeatedly until unique
            string newSubject;
            while (true)
            {
                newSubject = InputHelper.GetStringInput("Enter new subject: ").Trim().ToLower();

                if (existingSubjects.Contains(newSubject))
                {
                    Console.WriteLine("Subject already exists. Please enter a different one.");
                }
                else
                {
                    break;
                }
            }

            // Create new ID
            int newId = lines.Count; // since line 0 is header, line index = ID

            // Append new line to CSV
            string newLine = $"{newId},{newSubject}";
            File.AppendAllText(subject_path, Environment.NewLine + newLine);

            Console.WriteLine($"Subject '{newSubject}' added successfully!");
        }
        public void listStudentBySubject()
        {
            string subjectPath = "/Users/dangquangvinh/Desktop/COMP1551/subject.csv";
            string userPath = "/Users/dangquangvinh/Desktop/COMP1551/database.csv";

            // Load subjects
            List<string> subjectLines = File.ReadAllLines(subjectPath).ToList();

            if (subjectLines.Count <= 1)
            {
                Console.WriteLine("No subjects found.");
                return;
            }

            // Display subjects (skip header)
            Console.WriteLine("===== Choose a Subject =====");
            for (int i = 1; i < subjectLines.Count; i++)
            {
                string[] parts = subjectLines[i].Split(',');
                string subjectName = parts[1].Trim();
                Console.WriteLine($"{i}. {subjectName}");
            }

            int choice = InputHelper.GetIntInput("Enter subject number: ");

            if (choice < 1 || choice >= subjectLines.Count)
            {
                Console.WriteLine("Invalid choice.");
                return;
            }

            // Get selected subject name
            string[] chosenParts = subjectLines[choice].Split(',');
            string chosenSubject = chosenParts[1].Trim();

            Console.WriteLine($"\n===== STUDENTS TAKING {chosenSubject.ToUpper()} =====");

            // Load users
            List<string> userLines = File.ReadAllLines(userPath).ToList();

            bool found = false;

            for (int i = 1; i < userLines.Count; i++)
            {
                string[] fields = userLines[i].Split(',');

                string name = fields[0];
                string email = fields[2];
                string role = fields[3];

                // Student ?
                if (role != "Student") continue;

                string sub1 = fields[4];
                string sub2 = fields[5];
                string sub3 = fields[6];

                // Compare subjects
                if (sub1 == chosenSubject || sub2 == chosenSubject || sub3 == chosenSubject)
                {
                    Console.WriteLine($"- {name} ({email})");
                    found = true;
                }
            }

            if (!found)
            {
                Console.WriteLine("No students found for this subject.");
            }
        }
        public void listTeacherBySubject()
        {
            string userFile = "/Users/dangquangvinh/Desktop/COMP1551/database.csv";
            string subjectFile = "/Users/dangquangvinh/Desktop/COMP1551/subject.csv";

            // Load all subjects
            List<string> subjects = File.ReadAllLines(subjectFile).Skip(1).ToList(); // skip header
            Console.WriteLine("===== SUBJECT LIST =====");
            for (int i = 0; i < subjects.Count; i++)
            {
                string[] fields = subjects[i].Split(',');
                Console.WriteLine($"{i + 1}. {fields[1].Trim()}");
            }

            int subjectChoice = InputHelper.GetIntInput("Choose subject number: ");
            if (subjectChoice < 1 || subjectChoice > subjects.Count)
            {
                Console.WriteLine("Invalid subject selection.");
                return;
            }

            string chosenSubject = subjects[subjectChoice - 1].Split(',')[1].Trim();

            // Load all users
            List<string> users = File.ReadAllLines(userFile).Skip(1).ToList(); // skip header
            Console.WriteLine($"\n===== TEACHERS FOR {chosenSubject.ToUpper()} =====");
  

            bool found = false;
            foreach (string line in users)
            {
                string[] fields = line.Split(',');
                string role = fields[3].Trim();
                if (role == "Teacher")
                {
                    // subjects are in columns 4,5,6
                    if (fields.Skip(4).Take(3).Any(s => s.Trim() == chosenSubject))
                    {
                        Console.WriteLine($"{fields[0]} ({fields[2]})");
                        found = true;
                    }
                }
            }

            if (!found)
            {
                Console.WriteLine("No teachers found for this subject.");
            }
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            // csv file
            string path = "/Users/dangquangvinh/Desktop/COMP1551/database.csv"; 
            UserManager manager = new UserManager();

            while (true)
            {
                string choose = manager.chooseFunction();
                Console.Clear();

                switch (choose)
                {
                    case "1":
                        manager.handleAddUser(path);
                        break;
                    case "2":
                        manager.handleEditUser(path);
                        break;
                    case "3":
                        manager.handleDeleteUser(path);
                        break;
                    case "4":
                        manager.handleListAllUsers(path);
                        break;
                    case "5":
                        manager.handleListByRole(path);
                        break;
                    case "6":
                        manager.handleAddSubject();
                        break;
                    case "7":
                        manager.listStudentBySubject();
                    break;
                    case "8":
                        manager.listTeacherBySubject();
                    break;
                    case "0":
                        Console.WriteLine("Exiting program. Goodbye!");
                        return;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
                
                // Pause before showing the menu again, but only if an action was taken
                if (choose != "0" && choose != "") 
                {
                    Console.WriteLine("\nPress any key to return to menu...");
                    Console.ReadKey();
                }
                Console.Clear();
            }
        }
    }
}