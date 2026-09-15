# Project Backlog

## [ ] Story SACRA changes
**Requirements:**
Scope:
1. Hide the additional Prisoner fields (added via User Story 55120: Custodial - SACRA changes) on the Combined SACRA screen and limit users to selecting two prisoners only.
2. Change logic of generate combine sacra report before User can deciding to select Assessment result.

Scenario:
Scenario 1:
As user with permission to create SACRA Result, I can view  SACRA Result with 2 prisoner only by hiding additional column and hiding additional prisoner in sacra result panel.

1. Precondition
The logged in user as the user with permission to create SACRA Result
The selected prisoners have Individual SACRA.

2. Navigation
Risk / Assessment > SACRA Results

3. Validation
1. Updated on UI:
Currently SACRA result screen display as Layout 1
The screen is updated to:
Hide columns Prisoner C, D, E, F, G, H, I, J in grid of panel SACRA Result and keep Prisoner A and Prisoner B in grid
Hide Prisoner  search fields and PRN fields for Prisoner C, D, E, F, G, H, I, J. Redesign the UI as Layout 2

Scenario 2:
As user with permission to create SACRA Result, I can add SACRA Result with multiple prisoners after hide additional Prisoners.

1. Precondition
The logged in user as the user with permission to create SACRA Result
The selected prisoners have Individual SACRA.

2. Navigation
Risk / Assessment > SACRA Results

3. Validation
1. View combine SACRA report when inputting Prisoner A only:
a. Select Prisoner A and click Report Icon and select CombinedSACRA Report. System run Individual SACRA report and display in popup.
b. When the user closes the SACRA report and stay in Create SACRA screen.
c. When the user selects Prisoner A only and click Save. System display mandatory message for Prisoner B fields.

2. Add SACRA for 2 or more prisoners:
a. Default display:
In SACRA RESULT screen, SACRA Search Criteria Panel is expanded - as existing
Panel SACRA Result  is expanded - display the header of columns in Grid with fields in that panel. All fields are shown as blank - Refer to the Layout 2

b. When the users clicking on New icon, some fields are enabled for user to input information as below:
Prisoner A (mandatory)
Prisoner B (mandatory)
Assessment Result: Default value as Select (mandatory)
Additional Comments (mandatory)
c. Field 'Assessment Date' is auto populated the time the user clicks on New icon - always disabled (existing behavior).
d. When the user selects a prisoner in any Prisoner search field:
The corresponding PRN field will be auto-populated.
The PRN field is always be disabled (read-only).
e. There is no change to the existing behaviour of Progressive enabling of prisoner fields after the previous prisoner is selected. (This point is not in scope of Testing for this US, the code for this behavior is not changed)
f. Keep the existing behavior after selecting a prisoner in prisoner search field: Cannot remove the prisoner after selecting, user can only select other prisoner by clicking on search icon.
g. Keep the existing validation of existing fields in SACRA Result panel.

h. After the users select Prisoner field, click on report icon, click on the option CombinedSACRA Report. System will run SACRA report and display into popup while also saved the Sacra report document.
Print Preview Icon is enabled after running sacra report.
Click on Print Preview will display the Sacra report that you have to saved.
i. After user selects Prisoner fields and does not input other mandatory fields then click on Save:
Do not check validation of other validation field: Assessment result and Additional Comments (mandatory).
The system will show the confirmation popup message with message: "You must run the Combined SACRA report before you save the Assessment Result. Do you want to run the Combined SACRA report?" with Yes/ No button.
When the user clicks “YES” button: The system closes the confirmation popup, generates and displays the CombinedSACRA Report while also saved the document.
Next, the CombinedSACRA Report popup is closed and user remains in create SACRA screen.
Clicking Save without input mandatory fields, display inline mandatory message for required fields
Clicking Save with input all mandatory field, display standard saved message. Do NOT generate CombinedSACRA Report again.
When the user clicks NO: The system closes the confirmation popup. The user remains at create SACRA screen with all input data remains unchanged.
j. After the user inputs all mandatory fields and clicks Save without run CombinedSACRA Report and click Save:
The system will show the confirmation popup message with message: "You must run the Combined SACRA report before you save the Assessment Result. Do you want to run the Combined SACRA report?" with Yes/ No button.
When the user clicks “YES” option: The system closes the confirmation popup, generates and displays the CombinedSACRA Report while also saved the document.
Next, the CombinedSACRA Report popup is closed and the user remains at create SACRA screen.
Clicking Save without input mandatory fields display inline mandatory message for required fields
Clicking Save with input all mandatory field, display standard saved message. Do NOT generate CombinedSACRA Report again.
When the user clicks NO: The system closes the confirmation popup. The user remains at create SACRA screen with all input data remains unchanged.
k. After the user inputs all mandatory fields and then run CombinedSACRA Report and click Save:
Display standard saved successful message. 
Do NOT generate CombinedSACRA Report again. 
l. Keep the existing rule:
SACRA Result Record can be edited within 4 hours only.
Only the Creator may edit a SACRA Result.
m. Keep the other behaviors: The user can create multiple same SACRA Result with same prisoners.
n. Keep current validation If the user selects a prisoner in any prisoner field (e.g. Prisoner B, C, D…),
And the selected prisoner already exists in another prisoner field
Then the system must not allow duplicate selection to be accepted. And the system must immediately display the validation message "[Prisoner Field Label] cannot share a cell with himself".

3. Behavior of Print Preview icon:
a. In create mode, Print Preview icon is disabled.
b. After the user generated CombinedSACRA Report and does not saved the SACRA record: Print Preview icon is disabled.
c. After the user saved the SACRA record: Print Preview icon is enabled.
d. When the user selected any SACRA record in the grid: Print Preview icon is enabled.
e. When the user clicked on Print Preview icon, the system displays the latest saved generated CombinedSACRA Report when the user selected the Prisoner.

Scenario 3:
As user with permission to create SACRA Result, I can UPDATE SACRA Result with multiple prisoners after hide additional Prisoners.

1. Precondition
The logged in user as the user with permission to create SACRA Result
A SACRA Result record already exists with multiple prisoners
The logged in user is the creator
Created record is within 4 hours

2. Navigation
Risk / Assessment > SACRA Results

3. Validation
User can edit the created record by choosing the created record and click on Edit. When Edit icon is clicked, the system shall enable or disable fields based on the number of prisoners in the record as follows:

1. When the users edit two or more prisoners are recorded but do not change the Prisoner:
a. Prisoner A, B search field: enabled with inputted value (mandatory)
b. Prisoner C to J search fields: Must follow progressive enable logic: (NOT in Scope test of This User story - Refer to Scenario 1 above):
Prisoner C is enabled only when Prisoner B has value
Prisoner D is enabled only when Prisoner C has value
Continue sequentially until Prisoner J
c. Do not validation for generating CombinedSACRA Report due to prisoners are not changed.
d. The system shall update the record and display the corresponding values in the grid.
e. The system will show the standard successful message, then it will auto close or user can click on X button to close the successful pop up. 
DO NOT Generate new CombinedSACRA Report after the user saved updated data.
f. The footer is updated as the existing behavior.

2. When the users edit two or more prisoners are recorded include change the Prisoner selection:
a. Prisoner A, B search field: enabled with inputted value( mandatory)
When the user clicks on Save button: The system will show the confirmation popup message with message: "You must run the Combined SACRA report before you save the Assessment Result. Do you want to run the Combined SACRA report?" with Yes/ No button. 
When the user clicks “YES” option: The system closes the confirmation popup, generates and displays the CombinedSACRA Report while also saved the document.
Next, the CombinedSACRA Report popup is closed and the user remains at create SACRA screen.
Clicking Save without input mandatory fields, display inline mandatory message
Clicking Save with input all mandatory field, display standard saved message. Do NOT generate CombinedSACRA Report again.
When the user clicks NO: The system closes the confirmation popup. The user remains at create SACRA screen with all input data remains unchanged.
c. Print Preview icon is enabled and display the latest generated saved CombinedSACRA Report.
d. The system shall update the record and display the corresponding values in the grid.
e. The system will show the standard successful message, then it will auto closed or user can click on X button to close the successful pop up. 
DO NOT Generate new CombinedSACRA Report after the user saved updated data.
f. The footer is updated as the existing behavior. 

Change the Prisoner selection = Changing the prisoner selected for Prisoner A  AND/OR Prison B/Changing Prisoner A into Prisoner B/Changing Prisoner B into Prisoner A.



