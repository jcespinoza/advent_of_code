use std::collections::BTreeMap;

use itertools::Itertools;
use nom::{
    branch::alt,
    bytes::complete::{tag, is_a},
    character::{complete::{alpha1, newline}},
    multi::separated_list1,
    sequence::separated_pair,
    IResult,
};

pub fn process_part1(input: &str) -> u32 {
    let (_, cmds) = commands_parser(input).unwrap();

    let filesystem = build_filesystem(cmds);

    let dir_sizes = filesystem.dir_sizes();

    let sum_of_sizes = sum_dir_sizes_under(dir_sizes, 100000);

    sum_of_sizes
}

pub fn process_part2(input: &str) -> u32 {
  let disk_size:u32 = 70000000;
  let update_size: u32 = 30000000;

  let (_, cmds) = commands_parser(input).unwrap();

  let filesystem = build_filesystem(cmds);

  let dir_sizes = filesystem.dir_sizes();
  let used_space = dir_sizes.get ("").unwrap();
  
  let unused_size = disk_size - used_space;
  let target_size = update_size - unused_size;
  let candidate_dir_sizes = dir_sizes_over(dir_sizes, target_size);
  
    candidate_dir_sizes[0]
}

fn sum_dir_sizes_under(dir_sizes: BTreeMap<String, u32>, max_size: u32) -> u32 {
    let  sum_of_sizes:u32 =dir_sizes
    .iter()
    .filter(|(_,&size)| size < max_size)
    .map(|(_,size)| size)
    .sum();
    sum_of_sizes
}

fn dir_sizes_over(dir_sizes: BTreeMap<String, u32>, max_size: u32) -> Vec<u32> {
    let  target_sizes = dir_sizes
    .iter()
    .filter(|(_,&size)| size >= max_size)
    .map(|(_,&size)| size)
    .sorted().collect::<Vec<u32>>();

    target_sizes
}

fn build_filesystem(cmds: Vec<Command>) -> FileSystem {
    let mut directories: BTreeMap<String, Vec<File>> = BTreeMap::new();

    let mut route: Vec<&str> = vec![];

    for cmd in cmds.iter() {
        match cmd {
            Command::Cd(Cd::Root) => {
                route.push("");
            }
            Command::Cd(Cd::Up) => {
                route.pop();
            }
            Command::Cd(Cd::Into(dir)) => {
                route.push(dir);
            }
            Command::Ls(files) => {
                directories.entry(route.join("/")).or_insert(vec![]);

                for f in files.iter() {
                    match f {
                        LsResult::File { size, name } => {
                            directories.entry(route.join("/")).and_modify(|v| {
                                v.push(File {
                                    size: *size,
                                    name: name,
                                });
                            });
                        }
                        LsResult::Dir(_) => (),
                    }
                }
            }
        }
    }
    FileSystem { directories }
}

#[derive(Debug)]
struct FileSystem<'a> {
    directories: BTreeMap<String, Vec<File<'a>>>,
}

impl FileSystem<'_> {
    fn dir_sizes(&self) -> BTreeMap<String, u32> {
        let mut sizes: BTreeMap<String, u32> = BTreeMap::new();

        for (path, files) in self.directories.iter() {
            let child_dirs = path.split("/").collect::<Vec<&str>>();
            let current_size = files.iter().map(|f| f.size).sum::<u32>();

            for index in 0..child_dirs.len() {
                sizes
                    .entry(child_dirs[0..=index].join("/"))
                    .and_modify(|v| *v += current_size)
                    .or_insert(current_size);
            }
        }
        sizes
    }
}

fn commands_parser(input: &str) -> IResult<&str, Vec<Command>> {
    let (input, cmds) = separated_list1(newline, alt((ls_parser, cd_parser)))(input)?;

    Ok((input, cmds))
}

fn cd_parser(input: &str) -> IResult<&str, Command> {
    let (input, _) = tag("$ cd ")(input)?;
    let (input, dir_name) = alt((tag(".."), alpha1, tag("/")))(input)?;
    let cmd = match dir_name {
        "/" => Command::Cd(Cd::Root),
        ".." => Command::Cd(Cd::Up),
        name => Command::Cd(Cd::Into(name)),
    };
    Ok((input, cmd))
}

fn ls_parser(input: &str) -> IResult<&str, Command> {
    let (input, _) = tag("$ ls")(input)?;
    let (input, _) = newline(input)?;
    let (input, files) = separated_list1(newline, alt((file_parser, dir_parser)))(input)?;

    Ok((input, Command::Ls(files)))
}

fn file_parser(input: &str) -> IResult<&str, LsResult> {
    let (input, (size, name)) =
        separated_pair(nom::character::complete::u32, tag(" "), filename_parser)(input)?;
    Ok((input, LsResult::File { size, name }))
}

fn filename_parser(input: &str) -> IResult<&str, &str> {
    let (input, name) = 
    is_a("qwertyuiopasdfghjklzxcvbnm.")(input)?;

    Ok((input, name))
}

fn dir_parser(input: &str) -> IResult<&str, LsResult> {
    let (input, _) = tag("dir ")(input)?;
    let (input, name) = alpha1(input)?;
    Ok((input, LsResult::Dir(name)))
}

#[derive(Debug)]
struct File<'a> {
    name: &'a str,
    size: u32,
}

#[derive(Debug)]
enum Command<'a> {
    Cd(Cd<'a>),
    Ls(Vec<LsResult<'a>>),
}

#[derive(Debug)]
enum Cd<'a> {
    Up,
    Into(&'a str),
    Root,
}

#[derive(Debug)]
enum LsResult<'a> {
    File { size: u32, name: &'a str },
    Dir(&'a str),
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
    fn part2_works() {
        let result = process_part2(INPUT);
        assert_eq!(result, 24933642);
    }
}
