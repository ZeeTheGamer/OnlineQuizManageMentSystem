﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;
namespace Base_project
{
    public partial class CreateQuizParentWindow : Form
    {

        public static FlowLayoutPanel QuestionsListFlowLoayoutPanel;
        public CreateQuizParentWindow()
        {
            InitializeComponent();
            Console.WriteLine("New quiz creation window is created with topic name" + GlobalStaticVariablesAndMethods.currentTopicName);
            HideChild();
            QuestionsListFlowLoayoutPanel = flowLayoutPanelCreateQuizPanelShowAllListItemHolder;
          
           


        }
        private void CreateQuizParentWindow_Load(object sender, EventArgs e)
        {
            labelSubjectName.Text = "Topic Name: "+GlobalStaticVariablesAndMethods.currentTopicName;
            labelTopicName.Text ="Subject Name: "+ GlobalStaticVariablesAndMethods.currentSubjectName;
        }
       
        private void buttonCQAddNewQuestion_Click(object sender, EventArgs e)
        {

            panelAddNewQuestion.Show();
            panelAddNewQuestion.Location = new Point(33, 33);
            panelAddNewQuestion.BringToFront();
        }

        private void buttonCQShowAllQuestions_Click(object sender, EventArgs e)
        {
           HideChild();

           panelShowAllQuestions.Show();
           panelShowAllQuestions.Location = new Point(33, 33);
           panelShowAllQuestions.BringToFront();


            ArrayList QueztionsItems = new ArrayList();//Main list of questions.....


            DataTableReader dataTableReader = new DataTableReader(GlobalStaticVariablesAndMethods.currentDataSetUsedForHoldingQuestions.Tables[0]);
            
            Console.WriteLine(GlobalStaticVariablesAndMethods.currentDataSetUsedForHoldingQuestions.Tables[0].Rows.Count+" is used");
            QuizQuestionListItem quizQuestionListItem;
            ArrayList options;
            CheckBox checkBox;
            String answers="";
            String[] optionsValue;
            int listIndex = 0;
            while (dataTableReader.Read())
            {


                String topic = dataTableReader[1] as String;
                if (topic.Equals(GlobalStaticVariablesAndMethods.currentTopicName))
                {



                    quizQuestionListItem = new QuizQuestionListItem();
                    quizQuestionListItem.QuizSubject = GlobalStaticVariablesAndMethods.currentSubjectName;
                    quizQuestionListItem.QuizTitle = GlobalStaticVariablesAndMethods.currentTopicName;
                    quizQuestionListItem.QuizQuestionData = (string)dataTableReader[2];
                    quizQuestionListItem.TableRowId = dataTableReader[0]+"";
                    quizQuestionListItem.ListIndex = listIndex;
                    listIndex++;

                    options = new ArrayList();

                    answers = (string)dataTableReader[3];
                    optionsValue = answers.Split(';');
                    foreach (String opt in optionsValue)
                    {
                        checkBox = new CheckBox();
                        checkBox.Text = opt;
                        checkBox.Height = 25;
                        checkBox.Width = (opt.Length) * 30;
                        options.Add(checkBox);
                        if (opt.Equals(dataTableReader["RightAnswer"]))
                        {
                            checkBox.Checked = true;
                        }
                    }
                    quizQuestionListItem.Options = options;
                    QueztionsItems.Add(quizQuestionListItem);

                }
                
            }

            flowLayoutPanelCreateQuizPanelShowAllListItemHolder.Controls.Clear();
            foreach (QuizQuestionListItem questionListItem in QueztionsItems)
            {

                flowLayoutPanelCreateQuizPanelShowAllListItemHolder.Controls.Add(questionListItem);
                questionListItem.Width = flowLayoutPanelCreateQuizPanelShowAllListItemHolder.Width;
            }
           

        }

        private void buttonSaveQuizInSystem_Click(object sender, EventArgs e)
        {
            DatasetManager.saveQuizToDatabase();//save changes to database
            GlobalStaticVariablesAndMethods.isCurrentQuizTopicSaved = true;
            GlobalStaticVariablesAndMethods.CreateInfoMesssage(GlobalStaticVariablesAndMethods.ChangesSavedInfoMessage);

        }

        private void HideChild()
        {
            panelAddNewQuestion.Hide();
            panelShowAllQuestions.Hide();
        }
        
     

        private void panelShowAllQuestions_Paint(object sender, PaintEventArgs e)
        {

        }

        private void radioButtonFalseOption_CheckedChanged(object sender, EventArgs e)
        {

        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            this.Hide();
        }

       
    }
    }

