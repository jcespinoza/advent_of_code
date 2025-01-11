#!/bin/bash

# Read a day number and part number. Day must be a number between 1 and 25. Part can be either 1 or 2

# The script should be invoked like any of the following forms:
# ./run.sh --day 1 --part 1
# ./run.sh -d 1 -p 1
# ./run.sh 1 1 # In this case the day is the first argument and the part is the second argument

# Check if the first and second arguments are provided without flags
if [ $# -eq 2 ]; then
  DAY=$1
  PART=$2
  shift 2
fi

# If provided set the $DAY and $PART variables with the provided values
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
        --part=*)
            PART="${i#*=}"
            shift
            ;;
        --part)
            PART="$2"
            shift
            shift
            ;;
        -p=*)
            PART="${i#*=}"
            shift
            ;;
        -p)
            PART="$2"
            shift
            shift
            ;;
        *)
            ;;
    esac
done



## If arguments were not provided, ask the user for them
if [ -z "$DAY" ] && [ -z "$PART" ]; then
  read -p "Enter day number (1-25): " DAY
  read -p "Enter part number (1 or 2): " PART
elif [ -z "$DAY" ]; then
  read -p "Enter day number (1-25): " DAY
elif [ -z "$PART" ]; then
  read -p "Enter part number (1 or 2): " PART
fi


# Check if the day number is between 1 and 25
if [ $DAY -lt 1 ] || [ $DAY -gt 25 ]; then
    echo "Day number must be between 1 and 25"
    exit 1
fi

# Check if the part number is either 1 or 2
if [ $PART -ne 1 ] && [ $PART -ne 2 ]; then
    echo "Part number must be either 1 or 2"
    exit 1
fi

# Execute the binary with Cargo run
cargo run --quiet --bin advent-of-code -- -d $DAY -p $PART