use nom::{character::complete::{alpha1, newline}, character::{complete}, bytes::complete::{tag, take_until1}, IResult, branch::alt, sequence::separated_pair, multi::separated_list1};

pub fn process_part1(input: &str) -> u32 {
  let cmds = commands_parser(input).unwrap();
  dbg!(&cmds);

  0
}

pub fn process_part2(input: &str) -> u32 {
  0
}

fn commands_parser(input: &str) -> IResult<&str,Vec<Command>> {
  let (input, cmds) = separated_list1(newline, alt(
    (ls_parser, cd_parser)
  ))(input)?;
  
  Ok((input, cmds))
}

fn cd_parser(input: &str) -> IResult<&str,Command> {
  dbg!("cd", input);
  let (input, _) = tag("$ cd ")(input)?;
  let (input, dir_name) = alt((tag(".."), alpha1, tag("/") ))(input)?;
  let cmd = match dir_name {
    "/" => Command::Cd(Cd::Root),
    ".." => Command::Cd(Cd::Up),
    name => Command::Cd(Cd::Into(name))
  };
  Ok((input, cmd))
}

fn ls_parser(input: &str) -> IResult<&str,Command> {
  dbg!("ls", input);
  let (input, _) = tag("$ ls")(input)?;
  let (input, _) = newline(input)?;
  let (input, files) = separated_list1(newline, alt((file_parser, dir_parser)))(input)?;
  Ok((input, Command::Ls(files)))
}

fn file_parser(input: &str) -> IResult<&str,LsResult> {
  let (input, (size, name)) = separated_pair(
    nom::character::complete::u32,
     tag(" "),
     filename_parser
  )(input)?;
  Ok((input, LsResult::File{ size, name }))
}

fn filename_parser(input: &str) -> IResult<&str,&str> {
  let (input, name) = take_until1("\n")(input)?;
  Ok((input, name))
}

fn dir_parser(input: &str) -> IResult<&str,LsResult> {
  let (input, _) = tag("dir ")(input)?;
  let (input, name) = alpha1(input)?;
  Ok((input, LsResult::Dir(name)))
}


#[derive(Debug)]
enum Command<'a> {
  Cd(Cd<'a>),
  Ls(Vec<LsResult<'a>>),
}

#[derive(Debug)]
enum Cd<'a>{
  Up,
  Into(&'a str),
  Root,
}

#[derive(Debug)]
enum LsResult<'a> {
  File {size: u32, name: &'a str},
  Dir(&'a str)
}

#[cfg(test)]
mod tests {
  use super::*;
    const INPUT: &str = "$ cd /
$ ls
dir a
14848514 b.txt
8504156 c.dat
dir d
$ cd a
$ ls
dir e
29116 f
2557 g
62596 h.lst
$ cd e
$ ls
584 i
$ cd ..
$ cd ..
$ cd d
$ ls
4060174 j
8033020 d.log
5626152 d.ext
7214296 k";

  #[test]
  fn part1_works() {
    let result = process_part1(INPUT);
    assert_eq!(result, 95437);
  }

  #[test]
  #[ignore]
  fn part2_works() {
    let result = process_part2(INPUT);
    assert_eq!(result, 45000);
  }
}