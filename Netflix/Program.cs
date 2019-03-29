using System;
using System.Collections.Generic;
using System.IO;
using System.Linq; // Para el OrderByDescending method
using LucasLib;

namespace Netflix {
    class Program : ConsoleProgram {
        static void Main (string[] args) {
            Program program = new Program ();
            program.run ();
            program.pause ();
        }

        public void run () {
            QualificationRatingData data = new QualificationRatingData ();

            showMsg ("Diccionario:\n");
            foreach (var item in data.getUserQualifications ().OrderByDescending (pair => pair.Value).Take (10)) {
                showMsg ("User {" + item.Key + "} Qualifications {" + item.Value + "}");
            }
        }
    }

    class QualificationRatingData {
        private static readonly string DEFAULT_PATH = "res/ratings.txt";
        private Dictionary<int, int> userQualifications;
        public QualificationRatingData () {
            StreamReader objReader = new StreamReader (DEFAULT_PATH);
            string s = objReader.ReadLine ();
            if (s != null) {
                userQualifications = new Dictionary<int, int> ();
                while (s != null) {
                    Qualification qualification = new Qualification (s);
                    addUserQualification (qualification.getUserId ());
                    s = objReader.ReadLine ();
                }
            }
            objReader.Close ();
        }

        private void addUserQualification (int userId) {
            if (userQualifications.ContainsKey (userId))
                userQualifications[userId]++;
            else
                userQualifications.Add (userId, 1);
        }

        public Dictionary<int, int> getUserQualifications () {
            return this.userQualifications;
        }
    }

    class Qualification {
        private int movieId;
        private int userId;
        private int qualification;
        private DateTime qualificationDate;
        public Qualification (int movieId, int userId, int qualification, DateTime qualificationDate) {

        }

        public Qualification (string s) {
            string[] line = s.Split (",");
            this.movieId = Int32.Parse (line[0]);
            this.userId = Int32.Parse (line[1]);
            this.qualification = Int32.Parse (line[2]);
            this.qualificationDate = parseToDate (line[3]);
        }

        private DateTime parseToDate (string s) {
            string[] line = s.Split ("-");
            return new DateTime (Int32.Parse (line[0]), Int32.Parse (line[1]), Int32.Parse (line[2]));
        }

        override
        public string ToString () {
            return "MovieId: " + movieId + "\tUserId: " + userId + "\tQualification: " + qualification + "\tDate: " + qualificationDate.ToString () + "\n";
        }

        public int getUserId () {
            return this.userId;
        }
        public int getMovieId () {
            return this.movieId;
        }
        public int getQualification () {
            return this.qualification;
        }
        public DateTime GetDate () {
            return this.qualificationDate;
        }
    }
}