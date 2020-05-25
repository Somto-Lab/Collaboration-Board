# Collaboration-Board

Property of Somtochukwu Dibiaezue.

This application is created as a virtual representation of the existing Collaboration board used by Costain under the 
Severn Trent Framework. It requires an internet connection for effective operation

Users have the ability to create or load a "project" onto the "board". A project contains scheduled activities 
added by the user, wherein activities are automatically organised by date and duration.

Each activity is given a unique ID that assists in identification during modifications be it edits or deletion.

The user has the option to add a PDF file to view alongside the activities which may assist in effective scheduling. 

PDF code import uses base file from https://blogs.u2u.be/lander/post/2018/01/23/Creating-a-PDF-Viewer-in-WPF-using-Windows-10-APIs with
changes mine.

Activities designated as milestones are replicated on a collapsible left side panel.

All data is stored on and retrieved from a MongoDB server. For testing, server address needs to be replaced with IP address to be used.  

Suggestions on improvements are welcomed.
