// See http://www.codeproject.com/Articles/13922/Validate-user-input-in-Windows-Forms

namespace YoderZone.Extensions.Options.Itboy
{
partial class Validator
{
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        base.Dispose(disposing);

        if (disposing)
        {
            if (this.components != null)
            {
                this.components.Dispose();
                this.components = null;
            }

            if (this.errorProvider != null)
            {
                this.errorProvider.Dispose();
                this.errorProvider = null;
            }
        }
    }

    #region Component Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        this.components = new System.ComponentModel.Container();
    }

    #endregion
}
}
