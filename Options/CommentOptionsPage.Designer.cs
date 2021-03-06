﻿namespace YoderZone.Extensions.Options
{
using System.Windows.Controls;

using YoderZone.Extensions.Options;

partial class CommentOptionsPage
{
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (this.components != null))
        {
            this.components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Component Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        this.elementHost1 = new System.Windows.Forms.Integration.ElementHost();
        this.commentOptions1 = new
        CommentOptionsControl();
        this.SuspendLayout();
        //
        // elementHost1
        //
        this.elementHost1.Dock = System.Windows.Forms.DockStyle.Fill;
        this.elementHost1.Location = new System.Drawing.Point(3, 3);
        this.elementHost1.Name = "elementHost1";
        this.elementHost1.Size = new System.Drawing.Size(514, 498);
        this.elementHost1.TabIndex = 0;
        this.elementHost1.Text = "elementHost1";
        this.elementHost1.Child = this.commentOptions1;
        //
        // CommentOptionsPage
        //
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.Controls.Add(this.elementHost1);
        this.Name = "CommentOptionsPage";
        this.Padding = new System.Windows.Forms.Padding(3);
        this.Size = new System.Drawing.Size(520, 504);
        this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Integration.ElementHost elementHost1;
    private CommentOptionsControl commentOptions1;


}
}
