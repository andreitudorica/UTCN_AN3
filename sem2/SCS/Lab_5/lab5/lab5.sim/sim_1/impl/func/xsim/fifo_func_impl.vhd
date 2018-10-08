-- Copyright 1986-2017 Xilinx, Inc. All Rights Reserved.
-- --------------------------------------------------------------------------------
-- Tool Version: Vivado v.2017.4 (win64) Build 2086221 Fri Dec 15 20:55:39 MST 2017
-- Date        : Thu Apr 19 13:24:47 2018
-- Host        : Andrei-PC running 64-bit major release  (build 9200)
-- Command     : write_vhdl -mode funcsim -nolib -force -file
--               D:/Andrei/Scoala/SCS/Lab_5/lab5/lab5.sim/sim_1/impl/func/xsim/fifo_func_impl.vhd
-- Design      : fifo
-- Purpose     : This VHDL netlist is a functional simulation representation of the design and should not be modified or
--               synthesized. This netlist cannot be used for SDF annotated simulation.
-- Device      : xc7a35tcpg236-1
-- --------------------------------------------------------------------------------
library IEEE;
use IEEE.STD_LOGIC_1164.ALL;
library UNISIM;
use UNISIM.VCOMPONENTS.ALL;
entity debouncer is
  port (
    E : out STD_LOGIC_VECTOR ( 0 to 0 );
    wrinc_IBUF : in STD_LOGIC;
    clk_IBUF_BUFG : in STD_LOGIC
  );
end debouncer;

architecture STRUCTURE of debouncer is
  signal Q1 : STD_LOGIC;
  signal Q2 : STD_LOGIC;
  signal Q3 : STD_LOGIC;
begin
Q1_reg: unisim.vcomponents.FDRE
    generic map(
      INIT => '0'
    )
        port map (
      C => clk_IBUF_BUFG,
      CE => '1',
      D => wrinc_IBUF,
      Q => Q1,
      R => '0'
    );
Q2_reg: unisim.vcomponents.FDRE
    generic map(
      INIT => '0'
    )
        port map (
      C => clk_IBUF_BUFG,
      CE => '1',
      D => Q1,
      Q => Q2,
      R => '0'
    );
Q3_reg: unisim.vcomponents.FDRE
    generic map(
      INIT => '0'
    )
        port map (
      C => clk_IBUF_BUFG,
      CE => '1',
      D => Q2,
      Q => Q3,
      R => '0'
    );
q_out: unisim.vcomponents.LUT3
    generic map(
      INIT => X"40"
    )
        port map (
      I0 => Q3,
      I1 => Q1,
      I2 => Q2,
      O => E(0)
    );
end STRUCTURE;
library IEEE;
use IEEE.STD_LOGIC_1164.ALL;
library UNISIM;
use UNISIM.VCOMPONENTS.ALL;
entity fifo is
  port (
    clk : in STD_LOGIC;
    reset : in STD_LOGIC;
    rdinc : in STD_LOGIC;
    wrinc : in STD_LOGIC;
    rd : in STD_LOGIC;
    wr : in STD_LOGIC;
    data_in : in STD_LOGIC_VECTOR ( 7 downto 0 );
    data_out : out STD_LOGIC_VECTOR ( 7 downto 0 )
  );
  attribute NotValidForBitStream : boolean;
  attribute NotValidForBitStream of fifo : entity is true;
  attribute ECO_CHECKSUM : string;
  attribute ECO_CHECKSUM of fifo : entity is "4a8c46f0";
end fifo;

architecture STRUCTURE of fifo is
  signal clk1 : STD_LOGIC;
  signal clk1_i_1_n_0 : STD_LOGIC;
  signal clk1_reg_n_0 : STD_LOGIC;
  signal clk_IBUF : STD_LOGIC;
  signal clk_IBUF_BUFG : STD_LOGIC;
  signal count : STD_LOGIC_VECTOR ( 0 to 0 );
  signal \count[31]_i_10_n_0\ : STD_LOGIC;
  signal \count[31]_i_3_n_0\ : STD_LOGIC;
  signal \count[31]_i_4_n_0\ : STD_LOGIC;
  signal \count[31]_i_5_n_0\ : STD_LOGIC;
  signal \count[31]_i_6_n_0\ : STD_LOGIC;
  signal \count[31]_i_7_n_0\ : STD_LOGIC;
  signal \count[31]_i_8_n_0\ : STD_LOGIC;
  signal \count[31]_i_9_n_0\ : STD_LOGIC;
  signal \count_reg[12]_i_1_n_0\ : STD_LOGIC;
  signal \count_reg[16]_i_1_n_0\ : STD_LOGIC;
  signal \count_reg[20]_i_1_n_0\ : STD_LOGIC;
  signal \count_reg[24]_i_1_n_0\ : STD_LOGIC;
  signal \count_reg[28]_i_1_n_0\ : STD_LOGIC;
  signal \count_reg[4]_i_1_n_0\ : STD_LOGIC;
  signal \count_reg[8]_i_1_n_0\ : STD_LOGIC;
  signal \count_reg_n_0_[0]\ : STD_LOGIC;
  signal \count_reg_n_0_[10]\ : STD_LOGIC;
  signal \count_reg_n_0_[11]\ : STD_LOGIC;
  signal \count_reg_n_0_[12]\ : STD_LOGIC;
  signal \count_reg_n_0_[13]\ : STD_LOGIC;
  signal \count_reg_n_0_[14]\ : STD_LOGIC;
  signal \count_reg_n_0_[15]\ : STD_LOGIC;
  signal \count_reg_n_0_[16]\ : STD_LOGIC;
  signal \count_reg_n_0_[17]\ : STD_LOGIC;
  signal \count_reg_n_0_[18]\ : STD_LOGIC;
  signal \count_reg_n_0_[19]\ : STD_LOGIC;
  signal \count_reg_n_0_[1]\ : STD_LOGIC;
  signal \count_reg_n_0_[20]\ : STD_LOGIC;
  signal \count_reg_n_0_[21]\ : STD_LOGIC;
  signal \count_reg_n_0_[22]\ : STD_LOGIC;
  signal \count_reg_n_0_[23]\ : STD_LOGIC;
  signal \count_reg_n_0_[24]\ : STD_LOGIC;
  signal \count_reg_n_0_[25]\ : STD_LOGIC;
  signal \count_reg_n_0_[26]\ : STD_LOGIC;
  signal \count_reg_n_0_[27]\ : STD_LOGIC;
  signal \count_reg_n_0_[28]\ : STD_LOGIC;
  signal \count_reg_n_0_[29]\ : STD_LOGIC;
  signal \count_reg_n_0_[2]\ : STD_LOGIC;
  signal \count_reg_n_0_[30]\ : STD_LOGIC;
  signal \count_reg_n_0_[31]\ : STD_LOGIC;
  signal \count_reg_n_0_[3]\ : STD_LOGIC;
  signal \count_reg_n_0_[4]\ : STD_LOGIC;
  signal \count_reg_n_0_[5]\ : STD_LOGIC;
  signal \count_reg_n_0_[6]\ : STD_LOGIC;
  signal \count_reg_n_0_[7]\ : STD_LOGIC;
  signal \count_reg_n_0_[8]\ : STD_LOGIC;
  signal \count_reg_n_0_[9]\ : STD_LOGIC;
  signal data0 : STD_LOGIC_VECTOR ( 31 downto 1 );
  signal data_out_OBUF : STD_LOGIC_VECTOR ( 2 downto 0 );
  signal \data_out_TRI[0]\ : STD_LOGIC;
  signal plusOp : STD_LOGIC_VECTOR ( 2 downto 0 );
  signal rd_IBUF : STD_LOGIC;
  signal reset_IBUF : STD_LOGIC;
  signal wrinc_IBUF : STD_LOGIC;
  signal wrincout : STD_LOGIC;
  signal \NLW_count_reg[12]_i_1_CO_UNCONNECTED\ : STD_LOGIC_VECTOR ( 2 downto 0 );
  signal \NLW_count_reg[16]_i_1_CO_UNCONNECTED\ : STD_LOGIC_VECTOR ( 2 downto 0 );
  signal \NLW_count_reg[20]_i_1_CO_UNCONNECTED\ : STD_LOGIC_VECTOR ( 2 downto 0 );
  signal \NLW_count_reg[24]_i_1_CO_UNCONNECTED\ : STD_LOGIC_VECTOR ( 2 downto 0 );
  signal \NLW_count_reg[28]_i_1_CO_UNCONNECTED\ : STD_LOGIC_VECTOR ( 2 downto 0 );
  signal \NLW_count_reg[31]_i_2_CO_UNCONNECTED\ : STD_LOGIC_VECTOR ( 3 downto 0 );
  signal \NLW_count_reg[31]_i_2_O_UNCONNECTED\ : STD_LOGIC_VECTOR ( 3 to 3 );
  signal \NLW_count_reg[4]_i_1_CO_UNCONNECTED\ : STD_LOGIC_VECTOR ( 2 downto 0 );
  signal \NLW_count_reg[8]_i_1_CO_UNCONNECTED\ : STD_LOGIC_VECTOR ( 2 downto 0 );
  attribute SOFT_HLUTNM : string;
  attribute SOFT_HLUTNM of \count[0]_i_1\ : label is "soft_lutpair0";
  attribute SOFT_HLUTNM of \count[31]_i_5\ : label is "soft_lutpair0";
  attribute SOFT_HLUTNM of \wrptr[1]_i_1\ : label is "soft_lutpair1";
  attribute SOFT_HLUTNM of \wrptr[2]_i_1\ : label is "soft_lutpair1";
begin
DB3: entity work.debouncer
     port map (
      E(0) => wrincout,
      clk_IBUF_BUFG => clk_IBUF_BUFG,
      wrinc_IBUF => wrinc_IBUF
    );
clk1_i_1: unisim.vcomponents.LUT5
    generic map(
      INIT => X"FFFE0001"
    )
        port map (
      I0 => \count[31]_i_3_n_0\,
      I1 => \count[31]_i_4_n_0\,
      I2 => \count[31]_i_5_n_0\,
      I3 => \count[31]_i_6_n_0\,
      I4 => clk1_reg_n_0,
      O => clk1_i_1_n_0
    );
clk1_reg: unisim.vcomponents.FDRE
    generic map(
      INIT => '0'
    )
        port map (
      C => clk_IBUF_BUFG,
      CE => '1',
      D => clk1_i_1_n_0,
      Q => clk1_reg_n_0,
      R => '0'
    );
clk_IBUF_BUFG_inst: unisim.vcomponents.BUFG
     port map (
      I => clk_IBUF,
      O => clk_IBUF_BUFG
    );
clk_IBUF_inst: unisim.vcomponents.IBUF
     port map (
      I => clk,
      O => clk_IBUF
    );
\count[0]_i_1\: unisim.vcomponents.LUT1
    generic map(
      INIT => X"1"
    )
        port map (
      I0 => \count_reg_n_0_[0]\,
      O => count(0)
    );
\count[31]_i_1\: unisim.vcomponents.LUT4
    generic map(
      INIT => X"0001"
    )
        port map (
      I0 => \count[31]_i_3_n_0\,
      I1 => \count[31]_i_4_n_0\,
      I2 => \count[31]_i_5_n_0\,
      I3 => \count[31]_i_6_n_0\,
      O => clk1
    );
\count[31]_i_10\: unisim.vcomponents.LUT4
    generic map(
      INIT => X"FFDF"
    )
        port map (
      I0 => \count_reg_n_0_[13]\,
      I1 => \count_reg_n_0_[12]\,
      I2 => \count_reg_n_0_[15]\,
      I3 => \count_reg_n_0_[14]\,
      O => \count[31]_i_10_n_0\
    );
\count[31]_i_3\: unisim.vcomponents.LUT5
    generic map(
      INIT => X"FFFFBFFF"
    )
        port map (
      I0 => \count_reg_n_0_[19]\,
      I1 => \count_reg_n_0_[18]\,
      I2 => \count_reg_n_0_[16]\,
      I3 => \count_reg_n_0_[17]\,
      I4 => \count[31]_i_7_n_0\,
      O => \count[31]_i_3_n_0\
    );
\count[31]_i_4\: unisim.vcomponents.LUT5
    generic map(
      INIT => X"FFFFFFFE"
    )
        port map (
      I0 => \count_reg_n_0_[26]\,
      I1 => \count_reg_n_0_[27]\,
      I2 => \count_reg_n_0_[24]\,
      I3 => \count_reg_n_0_[25]\,
      I4 => \count[31]_i_8_n_0\,
      O => \count[31]_i_4_n_0\
    );
\count[31]_i_5\: unisim.vcomponents.LUT5
    generic map(
      INIT => X"FFFFFFFE"
    )
        port map (
      I0 => \count_reg_n_0_[2]\,
      I1 => \count_reg_n_0_[3]\,
      I2 => \count_reg_n_0_[0]\,
      I3 => \count_reg_n_0_[1]\,
      I4 => \count[31]_i_9_n_0\,
      O => \count[31]_i_5_n_0\
    );
\count[31]_i_6\: unisim.vcomponents.LUT5
    generic map(
      INIT => X"FFFFFEFF"
    )
        port map (
      I0 => \count_reg_n_0_[10]\,
      I1 => \count_reg_n_0_[11]\,
      I2 => \count_reg_n_0_[9]\,
      I3 => \count_reg_n_0_[8]\,
      I4 => \count[31]_i_10_n_0\,
      O => \count[31]_i_6_n_0\
    );
\count[31]_i_7\: unisim.vcomponents.LUT4
    generic map(
      INIT => X"FFFE"
    )
        port map (
      I0 => \count_reg_n_0_[21]\,
      I1 => \count_reg_n_0_[20]\,
      I2 => \count_reg_n_0_[23]\,
      I3 => \count_reg_n_0_[22]\,
      O => \count[31]_i_7_n_0\
    );
\count[31]_i_8\: unisim.vcomponents.LUT4
    generic map(
      INIT => X"FFFE"
    )
        port map (
      I0 => \count_reg_n_0_[29]\,
      I1 => \count_reg_n_0_[28]\,
      I2 => \count_reg_n_0_[31]\,
      I3 => \count_reg_n_0_[30]\,
      O => \count[31]_i_8_n_0\
    );
\count[31]_i_9\: unisim.vcomponents.LUT4
    generic map(
      INIT => X"FFFD"
    )
        port map (
      I0 => \count_reg_n_0_[5]\,
      I1 => \count_reg_n_0_[4]\,
      I2 => \count_reg_n_0_[7]\,
      I3 => \count_reg_n_0_[6]\,
      O => \count[31]_i_9_n_0\
    );
\count_reg[0]\: unisim.vcomponents.FDRE
    generic map(
      INIT => '1'
    )
        port map (
      C => clk_IBUF_BUFG,
      CE => '1',
      D => count(0),
      Q => \count_reg_n_0_[0]\,
      R => '0'
    );
\count_reg[10]\: unisim.vcomponents.FDRE
    generic map(
      INIT => '0'
    )
        port map (
      C => clk_IBUF_BUFG,
      CE => '1',
      D => data0(10),
      Q => \count_reg_n_0_[10]\,
      R => clk1
    );
\count_reg[11]\: unisim.vcomponents.FDRE
    generic map(
      INIT => '0'
    )
        port map (
      C => clk_IBUF_BUFG,
      CE => '1',
      D => data0(11),
      Q => \count_reg_n_0_[11]\,
      R => clk1
    );
\count_reg[12]\: unisim.vcomponents.FDRE
    generic map(
      INIT => '0'
    )
        port map (
      C => clk_IBUF_BUFG,
      CE => '1',
      D => data0(12),
      Q => \count_reg_n_0_[12]\,
      R => clk1
    );
\count_reg[12]_i_1\: unisim.vcomponents.CARRY4
     port map (
      CI => \count_reg[8]_i_1_n_0\,
      CO(3) => \count_reg[12]_i_1_n_0\,
      CO(2 downto 0) => \NLW_count_reg[12]_i_1_CO_UNCONNECTED\(2 downto 0),
      CYINIT => '0',
      DI(3 downto 0) => B"0000",
      O(3 downto 0) => data0(12 downto 9),
      S(3) => \count_reg_n_0_[12]\,
      S(2) => \count_reg_n_0_[11]\,
      S(1) => \count_reg_n_0_[10]\,
      S(0) => \count_reg_n_0_[9]\
    );
\count_reg[13]\: unisim.vcomponents.FDRE
    generic map(
      INIT => '0'
    )
        port map (
      C => clk_IBUF_BUFG,
      CE => '1',
      D => data0(13),
      Q => \count_reg_n_0_[13]\,
      R => clk1
    );
\count_reg[14]\: unisim.vcomponents.FDRE
    generic map(
      INIT => '0'
    )
        port map (
      C => clk_IBUF_BUFG,
      CE => '1',
      D => data0(14),
      Q => \count_reg_n_0_[14]\,
      R => clk1
    );
\count_reg[15]\: unisim.vcomponents.FDRE
    generic map(
      INIT => '0'
    )
        port map (
      C => clk_IBUF_BUFG,
      CE => '1',
      D => data0(15),
      Q => \count_reg_n_0_[15]\,
      R => clk1
    );
\count_reg[16]\: unisim.vcomponents.FDRE
    generic map(
      INIT => '0'
    )
        port map (
      C => clk_IBUF_BUFG,
      CE => '1',
      D => data0(16),
      Q => \count_reg_n_0_[16]\,
      R => clk1
    );
\count_reg[16]_i_1\: unisim.vcomponents.CARRY4
     port map (
      CI => \count_reg[12]_i_1_n_0\,
      CO(3) => \count_reg[16]_i_1_n_0\,
      CO(2 downto 0) => \NLW_count_reg[16]_i_1_CO_UNCONNECTED\(2 downto 0),
      CYINIT => '0',
      DI(3 downto 0) => B"0000",
      O(3 downto 0) => data0(16 downto 13),
      S(3) => \count_reg_n_0_[16]\,
      S(2) => \count_reg_n_0_[15]\,
      S(1) => \count_reg_n_0_[14]\,
      S(0) => \count_reg_n_0_[13]\
    );
\count_reg[17]\: unisim.vcomponents.FDRE
    generic map(
      INIT => '0'
    )
        port map (
      C => clk_IBUF_BUFG,
      CE => '1',
      D => data0(17),
      Q => \count_reg_n_0_[17]\,
      R => clk1
    );
\count_reg[18]\: unisim.vcomponents.FDRE
    generic map(
      INIT => '0'
    )
        port map (
      C => clk_IBUF_BUFG,
      CE => '1',
      D => data0(18),
      Q => \count_reg_n_0_[18]\,
      R => clk1
    );
\count_reg[19]\: unisim.vcomponents.FDRE
    generic map(
      INIT => '0'
    )
        port map (
      C => clk_IBUF_BUFG,
      CE => '1',
      D => data0(19),
      Q => \count_reg_n_0_[19]\,
      R => clk1
    );
\count_reg[1]\: unisim.vcomponents.FDRE
    generic map(
      INIT => '0'
    )
        port map (
      C => clk_IBUF_BUFG,
      CE => '1',
      D => data0(1),
      Q => \count_reg_n_0_[1]\,
      R => clk1
    );
\count_reg[20]\: unisim.vcomponents.FDRE
    generic map(
      INIT => '0'
    )
        port map (
      C => clk_IBUF_BUFG,
      CE => '1',
      D => data0(20),
      Q => \count_reg_n_0_[20]\,
      R => clk1
    );
\count_reg[20]_i_1\: unisim.vcomponents.CARRY4
     port map (
      CI => \count_reg[16]_i_1_n_0\,
      CO(3) => \count_reg[20]_i_1_n_0\,
      CO(2 downto 0) => \NLW_count_reg[20]_i_1_CO_UNCONNECTED\(2 downto 0),
      CYINIT => '0',
      DI(3 downto 0) => B"0000",
      O(3 downto 0) => data0(20 downto 17),
      S(3) => \count_reg_n_0_[20]\,
      S(2) => \count_reg_n_0_[19]\,
      S(1) => \count_reg_n_0_[18]\,
      S(0) => \count_reg_n_0_[17]\
    );
\count_reg[21]\: unisim.vcomponents.FDRE
    generic map(
      INIT => '0'
    )
        port map (
      C => clk_IBUF_BUFG,
      CE => '1',
      D => data0(21),
      Q => \count_reg_n_0_[21]\,
      R => clk1
    );
\count_reg[22]\: unisim.vcomponents.FDRE
    generic map(
      INIT => '0'
    )
        port map (
      C => clk_IBUF_BUFG,
      CE => '1',
      D => data0(22),
      Q => \count_reg_n_0_[22]\,
      R => clk1
    );
\count_reg[23]\: unisim.vcomponents.FDRE
    generic map(
      INIT => '0'
    )
        port map (
      C => clk_IBUF_BUFG,
      CE => '1',
      D => data0(23),
      Q => \count_reg_n_0_[23]\,
      R => clk1
    );
\count_reg[24]\: unisim.vcomponents.FDRE
    generic map(
      INIT => '0'
    )
        port map (
      C => clk_IBUF_BUFG,
      CE => '1',
      D => data0(24),
      Q => \count_reg_n_0_[24]\,
      R => clk1
    );
\count_reg[24]_i_1\: unisim.vcomponents.CARRY4
     port map (
      CI => \count_reg[20]_i_1_n_0\,
      CO(3) => \count_reg[24]_i_1_n_0\,
      CO(2 downto 0) => \NLW_count_reg[24]_i_1_CO_UNCONNECTED\(2 downto 0),
      CYINIT => '0',
      DI(3 downto 0) => B"0000",
      O(3 downto 0) => data0(24 downto 21),
      S(3) => \count_reg_n_0_[24]\,
      S(2) => \count_reg_n_0_[23]\,
      S(1) => \count_reg_n_0_[22]\,
      S(0) => \count_reg_n_0_[21]\
    );
\count_reg[25]\: unisim.vcomponents.FDRE
    generic map(
      INIT => '0'
    )
        port map (
      C => clk_IBUF_BUFG,
      CE => '1',
      D => data0(25),
      Q => \count_reg_n_0_[25]\,
      R => clk1
    );
\count_reg[26]\: unisim.vcomponents.FDRE
    generic map(
      INIT => '0'
    )
        port map (
      C => clk_IBUF_BUFG,
      CE => '1',
      D => data0(26),
      Q => \count_reg_n_0_[26]\,
      R => clk1
    );
\count_reg[27]\: unisim.vcomponents.FDRE
    generic map(
      INIT => '0'
    )
        port map (
      C => clk_IBUF_BUFG,
      CE => '1',
      D => data0(27),
      Q => \count_reg_n_0_[27]\,
      R => clk1
    );
\count_reg[28]\: unisim.vcomponents.FDRE
    generic map(
      INIT => '0'
    )
        port map (
      C => clk_IBUF_BUFG,
      CE => '1',
      D => data0(28),
      Q => \count_reg_n_0_[28]\,
      R => clk1
    );
\count_reg[28]_i_1\: unisim.vcomponents.CARRY4
     port map (
      CI => \count_reg[24]_i_1_n_0\,
      CO(3) => \count_reg[28]_i_1_n_0\,
      CO(2 downto 0) => \NLW_count_reg[28]_i_1_CO_UNCONNECTED\(2 downto 0),
      CYINIT => '0',
      DI(3 downto 0) => B"0000",
      O(3 downto 0) => data0(28 downto 25),
      S(3) => \count_reg_n_0_[28]\,
      S(2) => \count_reg_n_0_[27]\,
      S(1) => \count_reg_n_0_[26]\,
      S(0) => \count_reg_n_0_[25]\
    );
\count_reg[29]\: unisim.vcomponents.FDRE
    generic map(
      INIT => '0'
    )
        port map (
      C => clk_IBUF_BUFG,
      CE => '1',
      D => data0(29),
      Q => \count_reg_n_0_[29]\,
      R => clk1
    );
\count_reg[2]\: unisim.vcomponents.FDRE
    generic map(
      INIT => '0'
    )
        port map (
      C => clk_IBUF_BUFG,
      CE => '1',
      D => data0(2),
      Q => \count_reg_n_0_[2]\,
      R => clk1
    );
\count_reg[30]\: unisim.vcomponents.FDRE
    generic map(
      INIT => '0'
    )
        port map (
      C => clk_IBUF_BUFG,
      CE => '1',
      D => data0(30),
      Q => \count_reg_n_0_[30]\,
      R => clk1
    );
\count_reg[31]\: unisim.vcomponents.FDRE
    generic map(
      INIT => '0'
    )
        port map (
      C => clk_IBUF_BUFG,
      CE => '1',
      D => data0(31),
      Q => \count_reg_n_0_[31]\,
      R => clk1
    );
\count_reg[31]_i_2\: unisim.vcomponents.CARRY4
     port map (
      CI => \count_reg[28]_i_1_n_0\,
      CO(3 downto 0) => \NLW_count_reg[31]_i_2_CO_UNCONNECTED\(3 downto 0),
      CYINIT => '0',
      DI(3 downto 0) => B"0000",
      O(3) => \NLW_count_reg[31]_i_2_O_UNCONNECTED\(3),
      O(2 downto 0) => data0(31 downto 29),
      S(3) => '0',
      S(2) => \count_reg_n_0_[31]\,
      S(1) => \count_reg_n_0_[30]\,
      S(0) => \count_reg_n_0_[29]\
    );
\count_reg[3]\: unisim.vcomponents.FDRE
    generic map(
      INIT => '0'
    )
        port map (
      C => clk_IBUF_BUFG,
      CE => '1',
      D => data0(3),
      Q => \count_reg_n_0_[3]\,
      R => clk1
    );
\count_reg[4]\: unisim.vcomponents.FDRE
    generic map(
      INIT => '0'
    )
        port map (
      C => clk_IBUF_BUFG,
      CE => '1',
      D => data0(4),
      Q => \count_reg_n_0_[4]\,
      R => clk1
    );
\count_reg[4]_i_1\: unisim.vcomponents.CARRY4
     port map (
      CI => '0',
      CO(3) => \count_reg[4]_i_1_n_0\,
      CO(2 downto 0) => \NLW_count_reg[4]_i_1_CO_UNCONNECTED\(2 downto 0),
      CYINIT => \count_reg_n_0_[0]\,
      DI(3 downto 0) => B"0000",
      O(3 downto 0) => data0(4 downto 1),
      S(3) => \count_reg_n_0_[4]\,
      S(2) => \count_reg_n_0_[3]\,
      S(1) => \count_reg_n_0_[2]\,
      S(0) => \count_reg_n_0_[1]\
    );
\count_reg[5]\: unisim.vcomponents.FDRE
    generic map(
      INIT => '0'
    )
        port map (
      C => clk_IBUF_BUFG,
      CE => '1',
      D => data0(5),
      Q => \count_reg_n_0_[5]\,
      R => clk1
    );
\count_reg[6]\: unisim.vcomponents.FDRE
    generic map(
      INIT => '0'
    )
        port map (
      C => clk_IBUF_BUFG,
      CE => '1',
      D => data0(6),
      Q => \count_reg_n_0_[6]\,
      R => clk1
    );
\count_reg[7]\: unisim.vcomponents.FDRE
    generic map(
      INIT => '0'
    )
        port map (
      C => clk_IBUF_BUFG,
      CE => '1',
      D => data0(7),
      Q => \count_reg_n_0_[7]\,
      R => clk1
    );
\count_reg[8]\: unisim.vcomponents.FDRE
    generic map(
      INIT => '0'
    )
        port map (
      C => clk_IBUF_BUFG,
      CE => '1',
      D => data0(8),
      Q => \count_reg_n_0_[8]\,
      R => clk1
    );
\count_reg[8]_i_1\: unisim.vcomponents.CARRY4
     port map (
      CI => \count_reg[4]_i_1_n_0\,
      CO(3) => \count_reg[8]_i_1_n_0\,
      CO(2 downto 0) => \NLW_count_reg[8]_i_1_CO_UNCONNECTED\(2 downto 0),
      CYINIT => '0',
      DI(3 downto 0) => B"0000",
      O(3 downto 0) => data0(8 downto 5),
      S(3) => \count_reg_n_0_[8]\,
      S(2) => \count_reg_n_0_[7]\,
      S(1) => \count_reg_n_0_[6]\,
      S(0) => \count_reg_n_0_[5]\
    );
\count_reg[9]\: unisim.vcomponents.FDRE
    generic map(
      INIT => '0'
    )
        port map (
      C => clk_IBUF_BUFG,
      CE => '1',
      D => data0(9),
      Q => \count_reg_n_0_[9]\,
      R => clk1
    );
\data_out_OBUFT[0]_inst\: unisim.vcomponents.OBUFT
     port map (
      I => data_out_OBUF(0),
      O => data_out(0),
      T => \data_out_TRI[0]\
    );
\data_out_OBUFT[1]_inst\: unisim.vcomponents.OBUFT
     port map (
      I => data_out_OBUF(1),
      O => data_out(1),
      T => \data_out_TRI[0]\
    );
\data_out_OBUFT[2]_inst\: unisim.vcomponents.OBUFT
     port map (
      I => data_out_OBUF(2),
      O => data_out(2),
      T => \data_out_TRI[0]\
    );
\data_out_OBUFT[3]_inst\: unisim.vcomponents.OBUFT
     port map (
      I => '0',
      O => data_out(3),
      T => \data_out_TRI[0]\
    );
\data_out_OBUFT[4]_inst\: unisim.vcomponents.OBUFT
     port map (
      I => '0',
      O => data_out(4),
      T => \data_out_TRI[0]\
    );
\data_out_OBUFT[5]_inst\: unisim.vcomponents.OBUFT
     port map (
      I => '0',
      O => data_out(5),
      T => \data_out_TRI[0]\
    );
\data_out_OBUFT[6]_inst\: unisim.vcomponents.OBUFT
     port map (
      I => '0',
      O => data_out(6),
      T => \data_out_TRI[0]\
    );
\data_out_OBUFT[7]_inst\: unisim.vcomponents.OBUFT
     port map (
      I => '0',
      O => data_out(7),
      T => \data_out_TRI[0]\
    );
\data_out_OBUFT[7]_inst_i_1\: unisim.vcomponents.LUT1
    generic map(
      INIT => X"1"
    )
        port map (
      I0 => rd_IBUF,
      O => \data_out_TRI[0]\
    );
rd_IBUF_inst: unisim.vcomponents.IBUF
     port map (
      I => rd,
      O => rd_IBUF
    );
reset_IBUF_inst: unisim.vcomponents.IBUF
     port map (
      I => reset,
      O => reset_IBUF
    );
wrinc_IBUF_inst: unisim.vcomponents.IBUF
     port map (
      I => wrinc,
      O => wrinc_IBUF
    );
\wrptr[0]_i_1\: unisim.vcomponents.LUT1
    generic map(
      INIT => X"1"
    )
        port map (
      I0 => data_out_OBUF(0),
      O => plusOp(0)
    );
\wrptr[1]_i_1\: unisim.vcomponents.LUT2
    generic map(
      INIT => X"6"
    )
        port map (
      I0 => data_out_OBUF(0),
      I1 => data_out_OBUF(1),
      O => plusOp(1)
    );
\wrptr[2]_i_1\: unisim.vcomponents.LUT3
    generic map(
      INIT => X"78"
    )
        port map (
      I0 => data_out_OBUF(0),
      I1 => data_out_OBUF(1),
      I2 => data_out_OBUF(2),
      O => plusOp(2)
    );
\wrptr_reg[0]\: unisim.vcomponents.FDCE
    generic map(
      INIT => '0'
    )
        port map (
      C => clk1_reg_n_0,
      CE => wrincout,
      CLR => reset_IBUF,
      D => plusOp(0),
      Q => data_out_OBUF(0)
    );
\wrptr_reg[1]\: unisim.vcomponents.FDCE
    generic map(
      INIT => '0'
    )
        port map (
      C => clk1_reg_n_0,
      CE => wrincout,
      CLR => reset_IBUF,
      D => plusOp(1),
      Q => data_out_OBUF(1)
    );
\wrptr_reg[2]\: unisim.vcomponents.FDCE
    generic map(
      INIT => '0'
    )
        port map (
      C => clk1_reg_n_0,
      CE => wrincout,
      CLR => reset_IBUF,
      D => plusOp(2),
      Q => data_out_OBUF(2)
    );
end STRUCTURE;
