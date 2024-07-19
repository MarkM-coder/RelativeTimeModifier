<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="RelativeTimeModifier._default" %>

<!DOCTYPE html>

<html lang="en">
<head>
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <title>Relative Time Modifier demo</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.2.3/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-rbsA2VBKQhggwzxH7pPCaAqO46MgnOM80zW1RWuH61DGLwZJEdK2Kadq2F9CUG65" crossorigin="anonymous">
</head>
<body>
    <form runat="server">
        <div class="align-items-center">
            <div class="d-flex mt-5 mx-5">
                <label id="lblInsertInstruction"
                    for="txtYear"
                    class="col-form-label h3 px-3">
                    Insert Relative Time Modifier and hit GO! (is not case sensitive)
                </label>

                <asp:TextBox ID="txtRTM"
                    CssClass="form-control"
                    Width=" 300px" Text="now()"
                    runat="server">
                </asp:TextBox>

                <asp:Button ID="btnRTM"
                    CssClass="btn btn-primary ms-3"
                    runat="server"
                    Text="GO!"
                    OnClick="BtnRelativeTimeModifier_Click" />
            </div>

            <div>
                <div class="h4 mx-5 mt-3">
                    <asp:Label ID="lblError"
                        CssClass="h5"
                        ForeColor="Red"
                        runat="server"
                        Text="There is a problem with your syntax.  Please check and hit GO! again."
                        Visible="false">
                    </asp:Label>
                </div>
            </div>

            <div class="mt-5 mx-5">
                <div>
                    <h1>Your updated time is 
                        <span class="badge bg-secondary mt-3">
                            <asp:Label ID="lbl_time_display"
                                runat="server"
                                Text="Your updated Relative Time will appear here.">
                            </asp:Label>
                        </span></h1>
                </div>

                <div class="mt-5 mx-5">
                    <div class="mt-5 mb-3">
                        <asp:Label ID="lbl_time_saver"
                            CssClass="h3"
                            runat="server"
                            Text="Testing Time Savers">
                        </asp:Label>

                        <asp:Label ID="lbl_time_saver_instructions"
                            runat="server"
                            Text=" (click on each to populate text box)">
                        </asp:Label>
                    </div>

                    <div class="mt-4 mb-2">
                        <asp:Label ID="lbl_time_saver_standard"
                            CssClass="h5"
                            runat="server"
                            Text="Testing standard times - all render correctly">
                        </asp:Label>
                    </div>

                    <div>
                        <asp:Button ID="TimeSaverOne"
                            runat="server"
                            Text="now()+2y+3mon+4d+5h+6m+7s"
                            OnClick="TimeSaverOne_Click" />

                        <asp:Button ID="TimeSaverTwo"
                            runat="server"
                            Text="now()-2y-3mon-4d-5h-6m-7s"
                            OnClick="TimeSaverTwo_Click" />

                        <asp:Button ID="TimeSaverThree"
                            runat="server" Text="now()+2y-3mon+4d-5h+6m-7s"
                            OnClick="TimeSaverThree_Click" />

                        <asp:Button ID="TimeSaverFour"
                            runat="server" Text="now()+3mon+5h+7s"
                            OnClick="TimeSaverFour_Click" />

                        <asp:Button ID="TimeSaverFive"
                            runat="server"
                            Text="now()+3mon+5h+7s"
                            OnClick="TimeSaverFour_Click" />
                    </div>

                    <div class="mt-5 mb-2">
                        <asp:Label ID="lbl_time_saver_snap"
                            CssClass="h5"
                            runat="server"
                            Text="Testing snap times - all render correctly">
                        </asp:Label>
                    </div>
                    <div>
                        <asp:Button ID="TimeSaverSix"
                            runat="server"
                            Text="now()+2y+3mon+4d+5h+6m+7s@y"
                            OnClick="TimeSaverSix_Click" />

                        <asp:Button ID="TimeSaverSeven"
                            runat="server"
                            Text="now()-2y-3mon-4d-5h-6m-7s@mon"
                            OnClick="TimeSaverSeven_Click" />

                        <asp:Button ID="TimeSaverEight"
                            runat="server"
                            Text="now()+2y-3mon+4d-5h+6m-7s@d"
                            OnClick="TimeSaverEight_Click" />

                        <asp:Button ID="TimeSaverNine"
                            runat="server" Text="now()+3mon+5h+7s@h"
                            OnClick="TimeSaverNine_Click" />

                        <asp:Button ID="TimeSaverTen"
                            runat="server"
                            Text="now()+3mon+5h+7s@m"
                            OnClick="TimeSaverTen_Click" />

                        <div class="mt-2 mb-2">
                            <asp:Button ID="TimeSaverEleven"
                                runat="server"
                                Text="now()@y"
                                OnClick="TimeSaverEleven_Click" />

                            <asp:Button ID="TimeSaverTwelve"
                                runat="server"
                                Text="now()@mon"
                                OnClick="TimeSaverTwelve_Click" />

                            <asp:Button ID="TimeSaverThirteen"
                                runat="server"
                                Text="now()@d"
                                OnClick="TimeSaverThirteen_Click" />

                            <asp:Button ID="TimeSaverFourteen"
                                runat="server"
                                Text="now()@h"
                                OnClick="TimeSaverFourteen_Click" />

                            <asp:Button ID="TimeSaverFifteen"
                                runat="server"
                                Text="now()@m"
                                OnClick="TimeSaverFifteen_Click" />

                            <asp:Button ID="TimeSaverSixteen"
                                runat="server"
                                Text="now()@s"
                                OnClick="TimeSaverSixteen_Click" />
                        </div>
                    </div>

                    <div class="mt-5 mb-2">

                        <asp:Label ID="lbl_time_saver_standard_error"
                            CssClass="h5"
                            runat="server"
                            Text="Testing incorrectly formed standard times - all error">
                        </asp:Label>

                        <asp:Label ID="lbl_time_saver_standard_error_instructions"
                            runat="server"
                            Text=" (hover over each to identify the issue)">

                        </asp:Label>
                    </div>

                    <div>

                        <asp:Button ID="TimeSaverSeventeen"
                            runat="server"
                            ToolTip="missing + or - from 2y"
                            Text="now()2y+3mon+4d+5h+6m+7s"
                            OnClick="TimeSaverSeventeen_Click" />

                        <asp:Button ID="TimeSaverEighteen"
                            runat="server"
                            ToolTip="missing + or - from 3mon"
                            Text="now()-2y3mon-4d-5h-6m-7s"
                            OnClick="TimeSaverEighteen_Click" />

                        <asp:Button ID="TimeSaverNineteen"
                            runat="server"
                            ToolTip="missing the 'n' from from 3mon"
                            Text="now()+2y-3mo+4d-5h+6m-7s"
                            OnClick="TimeSaverNineteen_Click" />

                        <asp:Button ID="TimeSaverTwenty"
                            runat="server"
                            ToolTip="5 has no identifier character"
                            Text="now()+3mon+5+7s"
                            OnClick="TimeSaverTwenty_Click" />

                        <asp:Button ID="TimeSaverTwentyOne"
                            runat="server"
                            ToolTip="now has braces missing"
                            Text="now+3mon+5h+7s"
                            OnClick="TimeSaverTwentyOne_Click" />

                    </div>


                    <div class="mt-5 mb-2">
                        <asp:Label ID="lbl_time_saver_snap_error"
                            CssClass="h5"
                            runat="server"
                            Text="Testing incorrectly formed snap times - all error">
                        </asp:Label>

                        <asp:Label ID="lbl_time_saver_snap_error_instructions"
                            runat="server"
                            Text=" (hover over each to identify the issue)">
                        </asp:Label>
                    </div>
                    <div>
                        <asp:Button ID="TimeSaverTwentyTwo"
                            runat="server"
                            ToolTip="Missing character after @"
                            Text="now()+2y+3mon+4d+5h+6m+7s@"
                            OnClick="TimeSaverTwentyTwo_Click" />

                        <asp:Button ID="TimeSaverTwentyThree"
                            runat="server"
                            ToolTip="No separator after 6m"
                            Text="now()-2y-3mon-4d-5h-6m7s@mon"
                            OnClick="TimeSaverTwentyThree_Click" />

                        <asp:Button ID="TimeSaverTwentyFour"
                            runat="server"
                            ToolTip="'n' missing from 'mon'"
                            Text="now()+2y-3mo+4d-5h+6m-7s@d"
                            OnClick="TimeSaverTwentyFour_Click" />

                        <asp:Button ID="TimeSaverTwentyFive"
                            runat="server"
                            ToolTip="Bracket missing from now()"
                            Text="now(3mon+5h+7s@h"
                            OnClick="TimeSaverTwentyFive_Click" />

                        <asp:Button ID="TimeSaverTwentySix"
                            runat="server"
                            ToolTip="Extra character ('r') after 5h"
                            Text="now()+3mon+5hr+7s@m"
                            OnClick="TimeSaverTwentySix_Click" />

                        <div class="mt-2 mb-2">
                            <asp:Button ID="TimeSaverTwentySeven"
                                runat="server"
                                ToolTip="Extra character ('e') after 'y'"
                                Text="now()@ye"
                                OnClick="TimeSaverTwentySeven_Click" />

                            <asp:Button ID="TimeSaverTwentyEight"
                                runat="server"
                                ToolTip="'mon' mis-spelt"
                                Text="now()@mno"
                                OnClick="TimeSaverTwentyEight_Click" />

                            <asp:Button ID="TimeSaverTwentyNine"
                                runat="server"
                                ToolTip="Brackets missing from 'now()'"
                                Text="now@d"
                                OnClick="TimeSaverTwentyNine_Click" />

                            <asp:Button ID="TimeSaverThirty"
                                ToolTip="Random characters ('ss') inserted"
                                runat="server"
                                Text="now()ss@h"
                                OnClick="TimeSaverThirty_Click" />

                            <asp:Button ID="TimeSaverThirtyOne"
                                runat="server"
                                ToolTip="now() mis-spelt as 'mow()"
                                Text="mow()@m"
                                OnClick="TimeSaverThirtyOne_Click" />

                            <asp:Button ID="TimeSaverThirtyTwo"
                                runat="server"
                                ToolTip="Invalid character 's' inserted after @"
                                Text="now()s"
                                OnClick="TimeSaverThirtyTwo_Click" />
                        </div>
                    </div>
                </div>

            </div>
        </div>

        <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.2.3/dist/js/bootstrap.bundle.min.js" integrity="sha384-kenU1KFdBIe4zVF0s0G1M5b4hcpxyD9F7jL+jjXkk+Q2h455rYXK/7HAuoJl+0I4" crossorigin="anonymous"></script>
    </form>
</body>
</html>
