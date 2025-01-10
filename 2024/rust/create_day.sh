#!/bin/bash
# Check that the operating system is either windows or linux
echo "Checking operating system..."
if [ "$(uname)" == "Darwin" ]; then
    echo "This script is not supported on MacOS"
    exit 1
fi

# Get day number from the arguments in the form --day <integer> or -d <integer>
for i in "$@"
do
    case $i in
        --day=*)
            DAY="${i#*=}"
            shift
            ;;
        --day)
            DAY="$2"
            shift
            shift
            ;;
        -d=*)
            DAY="${i#*=}"
            shift
            ;;
        -d)
            DAY="$2"
            shift
            shift
            ;;
        *)
            ;;
    esac
done

# Provide usage details if the --help flag is passed
if [ "$1" == "--help" ] || [ "$1" == "-h" ]; then
    echo "Usage: create_day.sh --day <integer>"
    echo "Example: create_day.sh --day 1"
    exit 0
fi

# If no day is provided, exit with an error message
if [ -z "$DAY" ]; then
    echo "Please provide a day number using the --day flag"
    exit 1
fi

# set the dayname to the format Day<DAY> using 0-padding if the day is less than 10
DAYNAME=$(printf "%02d" $DAY)
echo "Creating files for Day $DAYNAME"

# Copy the templates/day-template folder into the new folder using the DAY variable as the folder name
# For example: day00

echo "Copying from template"
cp -r templates/day-template src/days/day$DAYNAME
echo "Copied day-template to src/days/day$DAYNAME"
# Append the contents of templates/mod.rs.txt to the end of src/days/mod.rs
echo "Appending module definitions to src/days/mod.rs"
cat templates/mod.rs.txt >> src/days/mod.rs

cd src/days # Go to the days folder
echo "Entering Days folder"

# Replace all instances of 00 with DAYNAME in the mod.rs file
sed -i "s/00/$DAYNAME/g" mod.rs
echo "Replaced all instances of 00 with $DAYNAME in mod.rs"

cd day$DAYNAME
echo "Entering day$DAYNAME folder"

# Rename the day00_impl.rs file to day<DAYNAME>_impl.rs
mv day00_impl.rs day${DAYNAME}_impl.rs
echo "Renamed day00_impl.rs to day${DAYNAME}_impl.rs"

# Rename the day00_lib.rs file to day<DAYNAME>_lib.rs
mv day00_lib.rs day${DAYNAME}_lib.rs
echo "Renamed day00_lib.rs to day${DAYNAME}_lib.rs"

# Rename the day00_solver.rs file to day<DAYNAME>_solver.rs
mv day00_solver.rs day${DAYNAME}_solver.rs
echo "Renamed day00_solver.rs to day${DAYNAME}_solver.rs"

# Rename the day00_tests.rs file to day<DAYNAME>_tests.rs
mv day00_tests.rs day${DAYNAME}_tests.rs
echo "Renamed day00_tests.rs to day${DAYNAME}_tests.rs"

# Replace all instances of 00 with DAYNAME in the day<DAYNAME>_impl.rs file
sed -i "s/00/$DAYNAME/g" day${DAYNAME}_impl.rs
echo "Replaced all instances of 00 with $DAYNAME in day${DAYNAME}_impl.rs"

# Replace all instances of 00 with DAYNAME in the day<DAYNAME>_lib.rs file
sed -i "s/00/$DAYNAME/g" day${DAYNAME}_lib.rs
echo "Replaced all instances of 00 with $DAYNAME in day${DAYNAME}_lib.rs"

# Replace all instances of 00 with DAYNAME in the day<DAYNAME>_solver.rs file
sed -i "s/00/$DAYNAME/g" day${DAYNAME}_solver.rs
echo "Replaced all instances of 00 with $DAYNAME in day${DAYNAME}_solver.rs"

# Replace all instances of 00 with DAYNAME in the day<DAYNAME>_tests.rs file
sed -i "s/00/$DAYNAME/g" day${DAYNAME}_tests.rs
echo "Replaced all instances of 00 with $DAYNAME in day${DAYNAME}_tests.rs"
sed -i "s/DAY_NUM: i32 = 0/DAY_NUM: i32 = $DAY/g" day${DAYNAME}_tests.rs
echo "Replaced value in DAY_NUM with $DAY in day${DAYNAME}_tests.rs"

cd .. # Go back to the days folder
echo "Exiting day$DAYNAME folder"
cd .. # Go back to the src folder
echo "Exiting days folder"

cd integration # Go to the integration folder
echo "Entering integration folder"


echo "Appending imports to solver_mapping.rs"
# Adding new import for the new day solver
BLANKS="  "
sed -i "/\/\/NEXT_IMPORT/i\\
${BLANKS}days::day${DAYNAME}::Day${DAYNAME}Solver," solver_mapping.rs

echo "Appending new DayNum entry"
# Adding new enum entry
BLANKS="  "
sed -i "/\/\/NEXT_ENUM_ENTRY/i\\
${BLANKS}Day${DAYNAME} = ${DAY}," solver_mapping.rs

echo "Appending new DayNum match entry"
# Adding new enum try_from entry
BLANKS="      "
sed -i "/\/\/NEXT_ENUM_TRY_FROM/i\\
${BLANKS}x if x == DayNum::Day${DAYNAME} as i32 => Ok(DayNum::Day${DAYNAME})," solver_mapping.rs

echo "Appending DayNum mapping for the new solver"
# Adding new enum match entry
BLANKS="    "
sed -i "/\/\/NEXT_ENUM_MATCH/i\\
${BLANKS}DayNum::Day${DAYNAME} => Box::new(Day${DAYNAME}Solver { day: ${DAY}, year })," solver_mapping.rs

cd .. # Go back to the src folder
echo "Exiting days folder"
cd .. # Go back to the root folder
echo "Exiting src folder"

# Create a branch and a commit for the new day
echo "Creating a branch and a commit for the new day"
git checkout -b day-$DAYNAME-rust24
git add .
echo "Added all files to the commit"
echo "Committing changes..."
git commit -m "Adding 2024 Day $DAYNAME in Rust"
echo "Changes committed. Ready to publish the branch"
echo "Start coding for Day $DAY!"