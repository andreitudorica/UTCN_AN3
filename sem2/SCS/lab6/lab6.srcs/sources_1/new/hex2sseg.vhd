----------------------------------------------------------------------------------
-- Company: 
-- Engineer: 
-- 
-- Create Date: 02.04.2018 12:32:49
-- Design Name: 
-- Module Name: hex2sseg - Behavioral
-- Project Name: 
-- Target Devices: 
-- Tool Versions: 
-- Description: 
-- 
-- Dependencies: 
-- 
-- Revision:
-- Revision 0.01 - File Created
-- Additional Comments:
-- 
----------------------------------------------------------------------------------


library IEEE;
use IEEE.STD_LOGIC_1164.ALL;

-- Uncomment the following library declaration if using
-- arithmetic functions with Signed or Unsigned values
--use IEEE.NUMERIC_STD.ALL;

-- Uncomment the following library declaration if instantiating
-- any Xilinx leaf cells in this code.
--library UNISIM;
--use UNISIM.VComponents.all;

entity hex2sseg is
    port ( hex : in std_logic_vector (3 downto 0);
           sseg : out std_logic_vector (6 downto 0));
end hex2sseg;

architecture Behavioral of hex2sseg is
type display_ROM is array (0 to 15) of std_logic_vector (6 downto 0);
constant convert_to_segments : display_ROM :=
--		("00000011","10011111","00100101","00001101","10011001","01001001","01000001","00011111",
--		 "00000001","00001001","00010000","00000000","01100010","00000010","01100000","01110000"); --0,1,2,3,4,5,6,7,8,9,A,B,C,D,E,F
        ("1000000","1111001","0100100","0110000","0011001","0010010","0000010","1111000",
		 "0000000","0010000","0001000","0000011","1000110","0100001","0000110","0001110"); --0,1,2,3,4,5,6,7,8,9,A,B,C,D,E,F
signal digit : integer := 0;
begin
    process(hex)
    begin
        case hex is
        when "0000" => digit <= 0;
        when "0001" => digit <= 1;
        when "0010" => digit <= 2;
        when "0011" => digit <= 3;
        when "0100" => digit <= 4;
        when "0101" => digit <= 5;
        when "0110" => digit <= 6;
        when "0111" => digit <= 7;
        when "1000" => digit <= 8;
        when "1001" => digit <= 9;
        when "1010" => digit <= 10;
        when "1011" => digit <= 11;
        when "1100" => digit <= 12;
        when "1101" => digit <= 13;
        when "1110" => digit <= 14;
        when "1111" => digit <= 15;
        end case;
       
        sseg <= convert_to_segments(digit);
    end process;
end Behavioral;
