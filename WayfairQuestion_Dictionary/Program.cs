using System;
using System.Collections.Generic;
using System.Linq;

/*
You are a developer for a university. Your current project is to develop a system for students to find courses they share with friends. The university has a system for querying courses students are enrolled in, returned as a list of (ID, course) pairs.

Write a function that takes in a collection of (student ID number, course name) pairs and returns, for every pair of students, a collection of all courses they share.


Sample Input:

student_course_pairs_1 = [
  ["58", "Linear Algebra"],
  ["94", "Art History"],
  ["94", "Operating Systems"],
  ["17", "Software Design"],
  ["58", "Mechanics"],
  ["58", "Economics"],
  ["17", "Linear Algebra"],
  ["17", "Political Science"],
  ["94", "Economics"],
  ["25", "Economics"],
  ["58", "Software Design"],
]

Sample Output (pseudocode, in any order):

find_pairs(student_course_pairs_1) =>
{
  "58,17": ["Software Design", "Linear Algebra"]
  "58,94": ["Economics"]
  "58,25": ["Economics"]
  "94,25": ["Economics"]
  "17,94": []
  "17,25": []
}



Additional test cases:

Sample Input:

student_course_pairs_2 = [
  ["0", "Advanced Mechanics"],
  ["0", "Art History"],
  ["1", "Course 1"],
  ["1", "Course 2"],
  ["2", "Computer Architecture"],
  ["3", "Course 1"],
  ["3", "Course 2"],
  ["4", "Algorithms"]
]



Sample output:

find_pairs(student_course_pairs_2) =>
{
  "1,0":[]
  "2,0":[]   
  "2,1":[]
  "3,0":[]
  "3,1":["Course 1", "Course 2"]
  "3,2":[]
  "4,0":[]
  "4,1":[]
  "4,2":[]
  "4,3":[]
} 

Sample Input:
student_course_pairs_3 = [
  ["23", "Software Design"], 
  ["3", "Advanced Mechanics"], 
  ["2", "Art History"], 
  ["33", "Another"],
]


Sample output:

find_pairs(student_course_pairs_3) =>
{
  "23,3": []
  "23,2": []
  "23,33":[]
  "3,2":  []
  "3,33": []
  "2,33": []
}

Complexity analysis variables:

n: number of student,course pairs in the input
s: number of students
c: total number of courses being offered (note: The number of courses any student can take is bounded by a small constant)
*/
class Solution
{
    static void Main(String[] args)
    {
        string[][] studentCoursePairs1 = new[]{
          new [] {"58", "Linear Algebra"},
          new [] {"94", "Art History"},
          new [] {"94", "Operating Systems"},
          new [] {"17", "Software Design"},
          new [] {"58", "Mechanics"},
          new [] {"58", "Economics"},
          new [] {"17", "Linear Algebra"},
          new [] {"17", "Political Science"},
          new [] {"94", "Economics"},
          new [] {"25", "Economics"},
          new [] {"58", "Software Design"},
        };

        var studentCoursePairsOutput1 = FindStudentCoursePairs(studentCoursePairs1);

        foreach (KeyValuePair<string, string[]> kvp in studentCoursePairsOutput1)
        {
            Console.WriteLine(string.Format("Key = {0}, Value = {1}", kvp.Key, string.Join(" ", kvp.Value)));
        }



        string[][] studentCoursePairs2 = new[]{
          new [] {"0", "Advanced Mechanics"},
          new [] {"0", "Art History"},
          new [] {"1", "Course 1"},
          new [] {"1", "Course 2"},
          new [] {"2", "Computer Architecture"},
          new [] {"3", "Course 1"},
          new [] {"3", "Course 2"},
          new [] {"4", "Algorithms"},
        };

        var studentCoursePairsOutput2 = FindStudentCoursePairs(studentCoursePairs2);

        foreach (KeyValuePair<string, string[]> kvp in studentCoursePairsOutput2)
        {
            Console.WriteLine(string.Format("Key = {0}, Value = {1}", kvp.Key, string.Join(" ", kvp.Value)));
        }

        string[][] studentCoursePairs3 = new[]{
          new [] {"23", "Software Design"},
          new [] {"3",  "Advanced Mechanics"},
          new [] {"2",  "Art History"},
          new [] {"33", "Another"},
        };

        var studentCoursePairsOutput3 = FindStudentCoursePairs(studentCoursePairs3);


        foreach (KeyValuePair<string, string[]> kvp in studentCoursePairsOutput3)
        {
            Console.WriteLine(string.Format("Key = {0}, Value = {1}", kvp.Key, string.Join(" ", kvp.Value)));
        }
    }

    private static Dictionary<string, string[]> FindStudentCoursePairs(string[][] studentCoursePairs)
    {
        Dictionary<string, string[]> studentCoursePairsOutput = new Dictionary<string, string[]>();

        var studentCourseLists = studentCoursePairs
                          .GroupBy(s => s[0], t => t[1]).ToArray()
                          .ToDictionary(g => g.Key, h => h.ToArray());


        foreach (var student1CourseList in studentCourseLists)
        {
            foreach (var student2CourseList in studentCourseLists)
            {
                if (!student1CourseList.Key.Equals(student2CourseList.Key))
                {
                    var studentPairKeys = student1CourseList.Key + " , " + student2CourseList.Key;
                    var reverseStudentPairKeys = student2CourseList.Key + " , " + student1CourseList.Key;

                    if (!studentCoursePairsOutput.ContainsKey(reverseStudentPairKeys))
                    {
                        var array1 = student1CourseList.Value;
                        var array2 = student2CourseList.Value;

                        var intersection = array1.Intersect(array2).ToArray();


                        studentCoursePairsOutput.Add(studentPairKeys, intersection ?? Array.Empty<string>());
                    }
                }

            }
        }

        return studentCoursePairsOutput;
    }
}
