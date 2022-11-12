namespace Silky.Zookeeper;

/// <summary>
/// Authentication type
/// </summary>
public enum AuthScheme
{
    /// <summary>
    /// only one belowid，Callanyone，world:anyoneon behalf of anyone，zookeeperThe node that has permission to everyone in theworld:anyoneType of。Default permissions for creating nodes。there is only oneidYesanyoneThe mode of authorization is world:anyone:rwadc Indicates that everyone hasrwadcpermission
    /// </summary>
    World = 0,
    
    /// <summary>
    ///unnecessaryid,只要Yes通过authenticationofuserhave authority（zookeepersupport throughkerberosto carry outauthencation, also supportsusername/password形式ofauthentication)
    /// </summary>
    Auth = 1,
    
    /// <summary>
    /// 它对应ofidforusername:BASE64(SHA1(password))，它需要先通过加密过ofusername:password形式ofauthentication。
    /// </summary>
    Digest = 2,
    
    /// <summary>
    ///它对应ofidfor客户机ofIPaddress，设置of时候可以设置一个ippart，for exampleip:192.168.1.0/16。
    /// </summary>
    Ip = 3,
    
    /// <summary>
    /// In this kind ofschemecase，对应ofidhave super authority，can do anything(cdrwa）
    /// </summary>
    Super = 4
}