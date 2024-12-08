#!/bin/bash
# Check that the operating system is either windows or linux
echo "Checking operating system..."
if [ "$(uname)" == "Darwin" ]; then
    echo "This script is not supported on MacOS"
    exit 1
fi

# Get day number from the arguments in the form --day=<integer> or -d=<integer>
for i in "$@"
do
case $i in
    --day=*|-d=*)
    DAY="${i#*=}"
    shift
    ;;
    *)
    ;;
esac
done

# set the dayname to the format Day<DAY> using 0-padding if the day is less than 10
DAYNAME=$(printf "%02d" $DAY)
echo "Creating files for Day $DAYNAME"

# Copy the DayTemplate folder into the new folder using the DAY variable as the folder name
# For example: Day00
cd Advent24.Days
echo "Entering Days folder"
cp -r DayTemplate Day$DAYNAME
echo "Copied DayTemplate to Day$DAYNAME"

cd Day$DAYNAME
echo "Entering Day$DAYNAME folder"

# Rename the Day00Solver.cs file to Day<DAYNAME>Solver.cs
mv Day00Solver.cs Day${DAYNAME}Solver.cs
echo "Renamed Day00Solver.cs to Day${DAYNAME}Solver.cs"

# Rename the Day00Tests.cs file to Day<DAYNAME>Test.cs
mv Day00Test.cs Day${DAYNAME}Test.cs
echo "Renamed Day00Test.cs to Day${DAYNAME}Test.cs"

# Replace all instances of 00 with DAYNAME in the Day<DAYNAME>Solver.cs file
sed -i "s/00/$DAYNAME/g" Day${DAYNAME}Solver.cs
echo "Replaced all instances of 00 with $DAYNAME in Day${DAYNAME}Solver.cs"

# Replace all instances of 00 with DAYNAME in the Day<DAYNAME>Test.cs file
sed -i "s/00/$DAYNAME/g" Day${DAYNAME}Test.cs
echo "Replaced all instances of 00 with $DAYNAME in Day${DAYNAME}Test.cs"

cd .. # Go back to the Days folder
echo "Exiting Day$DAYNAME folder"
cd .. # Go back to the solution folder
echo "Exiting Days folder"

# Create a branch and a commit for the new day
echo "Creating a branch and a commit for the new day"
git checkout -b day-$DAYNAME-csharp24
git add .
echo "Added all files to the commit"
echo "Committing changes..."
git commit -m "Adding 2024 Day $DAYNAME in C#"
echo "Changes committed. Ready to publish the branch"
echo "Start coding for Day $DAY!"