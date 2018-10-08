using System;
using System.Collections.Generic;
using System.Text;
using Assignment2.BLL.Models;
using Assignment2.DAL;

namespace Assignment2.BLL.Mappers
{
    class SubmissionMapper
    {
        public Submission map(SubmissionModel submission)
        {
            if (submission == null)
                return null;
            var newSubmission = new Submission() { ID = submission.ID, AssignmentID = submission.AssignmentID, GitLink = submission.GitLink, Grade = submission.Grade, Note = submission.Note, StudentID = submission.StudentID };
            return newSubmission;
        }
        public SubmissionModel map(Submission submission)
        {
            if (submission == null)
                return null;
            var newSubmission = new SubmissionModel() { ID = submission.ID, AssignmentID = submission.AssignmentID, GitLink = submission.GitLink, Grade = submission.Grade, Note = submission.Note, StudentID = submission.StudentID };
            return newSubmission;
        }
    }
}
