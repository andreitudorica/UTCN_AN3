----------------------------------------------------------------------------------
-- Company: 
-- Engineer: 
-- 
-- Create Date: 14.05.2018 09:13:42
-- Design Name: 
-- Module Name: Mem64k8b - Behavioral
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
use IEEE.NUMERIC_STD.ALL;

-- Uncomment the following library declaration if instantiating
-- any Xilinx leaf cells in this code.
--library UNISIM;
--use UNISIM.VComponents.all;

entity Mem64k8b is
    Port (data : inout std_logic_vector(7 downto 0); address : in std_logic_vector(15 downto 0); nCS : in std_logic; nWR : in std_logic );
end Mem64k8b;

architecture Behavioral of Mem64k8b is

type ram_t is array (0 to 65535) of std_logic_vector(7 downto 0);
signal ram : ram_t := (others => (others => '0'));

begin

process(address, data, nCS, nWR)
variable index:integer;
begin
    if nCS = '0' then
        index := to_integer(unsigned(address));
        if nWR = '0' then
            ram(index) <= data;
        else
            data <= ram(index);
        end if;
    else
        data<="ZZZZZZZZ";
    end if;
end process;
    
end Behavioral;
