use nom::{
    bytes::complete::tag,
    character::complete::{self, newline},
    multi::separated_list1,
    sequence::separated_pair,
    IResult,
};

#[derive(Debug)]
struct PairAssignment {
    lower_bound_1: u32,
    higher_bound_1: u32,
    lower_bound_2: u32,
    higher_bound_2: u32,
}

fn section_range(input: &str) -> IResult<&str, (u32, u32)> {
    let (input, (start, end)) = separated_pair(complete::u32, tag("-"), complete::u32)(input)?;

    Ok((input, (start, end)))
}

fn pair_assignment(input: &str) -> IResult<&str, PairAssignment> {
    let (input, (pair1, pair2)) = separated_pair(section_range, tag(","), section_range)(input)?;

    Ok((
        input,
        PairAssignment {
            lower_bound_1: pair1.0,
            higher_bound_1: pair1.1,
            lower_bound_2: pair2.0,
            higher_bound_2: pair2.1,
        },
    ))
}

fn assignments_collection(input: &str) -> IResult<&str, Vec<PairAssignment>> {
    let (input, ranges) = separated_list1(newline, pair_assignment)(input)?;

    Ok((input, ranges))
}

pub fn process_part1(input: &str) -> u32 {
    let (_, assignments) = assignments_collection(input).unwrap();

    let groups_with_overlaps = assignments.iter().filter(|assignment| {
      assignment.lower_bound_1 <= assignment.lower_bound_2 && assignment.higher_bound_1 >= assignment.higher_bound_2
      ||
      assignment.lower_bound_2 <= assignment.lower_bound_1 && assignment.higher_bound_2 >= assignment.higher_bound_1

    }).count();
    
    groups_with_overlaps as u32
}

pub fn process_part2(input: &str) -> u32 {
    0
}

#[cfg(test)]
mod tests {
    use super::*;
    const INPUT: &str = "2-4,6-8
2-3,4-5
5-7,7-9
2-8,3-7
6-6,4-6
2-6,4-8";

    #[test]
    fn part1_works() {
        let result = process_part1(INPUT);
        assert_eq!(result, 2);
    }

    #[test]
    #[ignore]
    fn part2_works() {
      let result = process_part2(INPUT);
      assert_eq!(result, 45000);
    }
}
